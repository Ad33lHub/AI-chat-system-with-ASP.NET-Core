# VerixSoft ASP.NET Core MVC Project Instructions

## 1. Prerequisites
Before running this project on a new device, ensure you have the following installed:
- **.NET 8 SDK**: [Download Here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server Express** or **LocalDB** (Usually installed with Visual Studio).

## 2. Project Setup
1.  **Copy the Project**:
    Copy the entire `VerixSoftMvc` folder to your new computer.

2.  **Open in Terminal**:
    Open Command Prompt, PowerShell, or Terminal and navigate to the project folder:
    ```powershell
    cd path/to/VerixSoftMvc
    ```

## 3. Database Setup
You have two options to set up the database. **Choose only one**.

### Option A: Automatic Setup (Recommended)
This method automatically creates the database and seeds it with data using the code.
Run this command in your terminal:
```powershell
dotnet ef database update
```
*Note: If you get an error saying `dotnet ef` is not found, run `dotnet tool install --global dotnet-ef` first.*

### Option B: Manual SQL Import
If you prefer to use SQL Server Management Studio (SSMS):
1.  Locate the file `database.sql` in this folder.
2.  Open SSMS and connect to your SQL Server instance.
3.  Open `database.sql` and execute the script.
4.  **Important**: Check `appsettings.json` and update the `ConnectionStrings` to match your SQL Server instance name if it is not `(localdb)\mssqllocaldb`.

## 4. Running the Application
Once the database is ready, run the application:
```powershell
dotnet run
```
The terminal will show a URL (e.g., `http://localhost:5123`). Open this URL in your browser.

## 5. Project Structure
- **Controllers/**: Handles the logic (e.g., `HomeController.cs`).
- **Models/**: Defines the data structure (e.g., `Service`, `TechStack`).
- **Views/**: Contains the HTML pages (`Home/Index.cshtml`).
- **wwwroot/**: Contains static files like CSS (`css/site.css`) and images.
- **appsettings.json**: Contains database connection strings.

## Troubleshooting
- **Database Connection Error**: Verify `appsettings.json` connection string matches your machine's SQL Server name.
- **Styling Issues**: Ensure `wwwroot/css/site.css` is present.
