using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace VerixSoftMvc.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _intentResponses;
        private readonly string _pythonUrl = "http://127.0.0.1:5000/predict";

        public ChatService(IWebHostEnvironment env)
        {
            _httpClient = new HttpClient();
            _intentResponses = LoadResponses(Path.Combine(env.ContentRootPath, "model-train", "verixsoft_chat_dataset.csv"));
        }

        private Dictionary<string, string> LoadResponses(string csvPath)
        {
            var responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!File.Exists(csvPath)) return responses;

            var lines = File.ReadAllLines(csvPath);
            // Skip header
            foreach (var line in lines.Skip(1))
            {
                // Simple CSV parsing (handling generated CSV structure)
                // Format: ID,Intent,User_Query,System_Instruction,Ideal_Response,Source
                // We need Intent -> Ideal_Response. 
                // Since multiple queries map to same intent, we just need one response per intent.
                // We'll take the first one we find for simplicity or prioritize.
                
                var parts = ParseCsvLine(line);
                if (parts.Count < 5) continue;

                var intent = parts[1];
                var response = parts[4];

                if (!responses.ContainsKey(intent))
                {
                    responses[intent] = response;
                }
            }
            return responses;
        }

        private List<string> ParseCsvLine(string line)
        {
            // Basic CSV parser to handle quotes
            var result = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            foreach (var c in line)
            {
                if (c == '\"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            result.Add(current.ToString());
            return result;
        }

        public async Task<string> GetResponseAsync(string userMessage)
        {
            try
            {
                var payload = new { text = userMessage };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_pythonUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    return "I'm having trouble connecting to my brain right now. Please try again later.";
                }

                var responseString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;
                
                string intent = root.GetProperty("intent").GetString();
                double confidence = root.GetProperty("confidence").GetDouble();

                if (confidence < 0.40) // Threshold for "I don't know"
                {
                    return "I'm not sure I understand. Could you rephrase that? You can ask me about Verixsoft's services, team, or location.";
                }

                if (_intentResponses.TryGetValue(intent, out var reply))
                {
                    return reply;
                }

                return "I understood the intent (" + intent + ") but I don't have a specific response mapped for it yet.";
            }
            catch (Exception)
            {
                return "System is currently offline or unreachable. Please contact support@verixsoft.com.";
            }
        }
    }
}
