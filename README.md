# VerixSoft AI-Integrated Chat Support System

A comprehensive Intelligent Chat Support System built with **ASP.NET Core MVC** for the frontend and a **Python (Flask)** backend for the AI model. This project integrates a machine learning chatbot to provide automated support responses.

## 🚀 Features

*   **ASP.NET Core MVC**: Robust web application framework.
*   **Python AI Backend**: Specialized Flask server running a Keras/TensorFlow model for natural language processing.
*   **SQL Server Integration**: Reliable data storage for application content.
*   **Real-time Communication**: Seamless interaction between the .NET web app and the Python AI service.

## 📋 Prerequisites

Before setting up the project, ensure you have the following installed:

1.  **.NET SDK**: 
    *   The project is configured for a newer .NET version (check `VerixSoftMvc.csproj`). We recommend installing the latest **.NET 8.0 SDK** or newer.
    *   [Download .NET SDK](https://dotnet.microsoft.com/download)
2.  **Python 3.10+**:
    *   Required for running the AI Chat Model.
    *   **Important**: Check "Add Python to PATH" during installation.
    *   [Download Python](https://www.python.org/downloads/)
3.  **SQL Server**:
    *   **LocalDB** (included with Visual Studio) or **SQL Server Express**.
    *   [Download SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## 🛠️ Installation & Setup

Follow these steps to get the project running on your local machine.

### 1. Setup the AI Backend (Python)
The chatbot logic resides in the `model-train` directory. It must be running for the chat feature to work.

1.  Open a terminal and navigate to the model directory:
    ```powershell
    cd model-train
    ```
2.  Install the required Python libraries:
    ```powershell
    pip install flask tensorflow numpy scikit-learn pandas
    ```
3.  Start the AI Server:
    ```powershell
    python chat_server.py
    ```
    ✅ **Success**: You should see `Running on http://127.0.0.1:5000`.
    *Keep this terminal window open.*

### 2. Setup the Web Application (.NET)
1.  Open a **new** terminal in the project root (`VerixSoftMvc` folder).
2.  **Configure Database**:
    *   Open `appsettings.json`.
    *   Check the `ConnectionStrings`. The default is set for **LocalDB**:
        ```json
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VerixSoftDb;Trusted_Connection=True;MultipleActiveResultSets=true"
        ```
    *   If using SQL Server Express, update `Server` to `.\\SQLEXPRESS` or `localhost`.
3.  **Restore & Update Database**:
    Run the following commands to install dependencies and create the database schema:
    ```powershell
    dotnet restore
    dotnet tool install --global dotnet-ef
    dotnet ef database update
    ```
    *Note: If `dotnet ef` fails, ensure the tool is installed globally.*

### 3. Run the Application
In the root terminal (where you ran the database update), start the web app:

```powershell
dotnet run
```

Open your browser and navigate to the URL shown (e.g., `http://localhost:5038`).

## 📂 Project Structure

*   **`VerixSoftMvc/`**: The main ASP.NET Core MVC application.
    *   `Controllers/`: Handles web requests.
    *   `Views/`: Razor pages for the UI.
    *   `Data/`: Entity Framework Core database context.
    *   `wwwroot/`: Static assets (CSS, JS, Images).
*   **`model-train/`**: The Python AI environment.
    *   `chat_server.py`: Flask API server for the chatbot.
    *   `intents.json` / `data.csv`: Training data for the model.

## ❓ Troubleshooting

| Issue | Solution |
| :--- | :--- |
| **"I'm not sure I understand" (Chatbot Loop)** | The AI server might need a restart. Stop the Python script (Ctrl+C) and run `python chat_server.py` again to reload the model. |
| **Database Connection Errors** | Verify your `appsettings.json` connection string matches your installed SQL Server instance (LocalDB vs Express). |
| **Styling Issues** | Ensure the `wwwroot` folder exists and contains CSS/JS files. |

---
*Generated for VerixSoftMvc Project*
