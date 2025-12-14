# VerixSoft AI-Integrated Project Migration Instructions

Here is the complete guide to migrating, setting up, and running this project (ASP.NET Core + Python AI Chatbot) on a new device.

## 1. Prerequisites (Tools Needed)
Before you begin, install the following on your new machine:

1.  **.NET 8.0 SDK** (or later): [Download Here](https://dotnet.microsoft.com/download/dotnet/8.0)
2.  **Python 3.10+**: [Download Here](https://www.python.org/downloads/)
    *   *Important*: Check "Add Python to PATH" during installation.
3.  **SQL Server** (LocalDB or Express):
    *   Usually installed with Visual Studio via the "ASP.NET and web development" workload.
    *   Or install [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

---

## 2. Python AI Environment Setup
The chatbot requires a Python environment to run the Keras model.

1.  **Navigate to the Model Directory**:
    Open a terminal/command prompt and go to the `model-train` folder inside the project:
    ```powershell
    cd path\to\VerixSoftMvc\model-train
    ```

2.  **Install Dependencies**:
    Run the following command to install required libraries:
    ```powershell
    pip install flask tensorflow numpy scikit-learn pandas
    ```

3.  **Start the AI Server**:
    Keep this terminal open and run:
    ```powershell
    python chat_server.py
    ```
    *   You should see: `Running on http://127.0.0.1:5000`
    *   **Note**: This window must remain open for the chatbot to reply.

---

## 3. Database Setup (SQL Server)
Configure the database for the website content.

1.  **Check Connection String**:
    Open `appsettings.json` in the root `VerixSoftMvc` folder. Ensure the `DefaultConnection` matches your SQL Server instance. Default is:
    ```json
    "Server=(localdb)\\mssqllocaldb;Database=VerixSoftDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    ```

2.  **Apply Migrations**:
    Open a **new** terminal window in the root `VerixSoftMvc` folder (not `model-train`) and run:
    ```powershell
    dotnet ef database update
    ```
    *   If `dotnet ef` is not found, run: `dotnet tool install --global dotnet-ef` first.

---

## 4. Running the Website
With the Python server running in pending window and database set up:

1.  **Run the Project**:
    In the `VerixSoftMvc` root terminal:
    ```powershell
    dotnet run
    ```

2.  **Browse**:
    Open the URL shown (e.g., `http://localhost:5038`) in your browser.

---

## 5. Troubleshooting Qualities

### Chatbot says "I'm not sure I understand" constantly?
- **Fix**: Restart the Python server. The server has a self-healing mechanism that rebuilds the AI encoder from the CSV file if the cached model is mismatched.
- **Command**: Stop the python script (Ctrl+C) and run `python chat_server.py` again.

### Database Errors?
- **Fix**: Ensure your `appsettings.json` connection string is correct and that you have run `dotnet ef database update`.

### Missing Styling/Images?
- **Fix**: Ensure you copied the `wwwroot` folder completely.
