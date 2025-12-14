using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VerixSoftMvc.Services;

namespace VerixSoftMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var response = await _chatService.GetResponseAsync(request.Message);
            return Ok(new { response });
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
    }
}
