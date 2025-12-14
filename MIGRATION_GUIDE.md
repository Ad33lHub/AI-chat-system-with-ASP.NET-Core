# Project Migration Guide: VerixSoft ASP.NET Core MVC

This guide details the steps, tools, and considerations required to migrate the **VerixSoftMvc** project from one device (source) to another (target).

## 1. Required Tools & Prerequisites
Before copying the project, ensure the **Target Device** has the following tools installed:

### Essential Runtimes & SDKs
*   **.NET 8.0 SDK (or later)**: Required to build and run the application.
    *   **Check**: Open terminal and run `dotnet --version`.
    *   **Install**: [Download .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
*   **SQL Server**: A database engine is required.
    *   **Option A (Recommended for Dev)**: **SQL Server Express LocalDB**. usually included with Visual Studio.
    *   **Option B**: **SQL Server Express** (Full instance).
    *   **Option C**: **Docker** container running MSSQL.

### Editors (Optional but Recommended)
*   **Visual Studio 2022**: With "ASP.NET and web development" workload.
*   **Visual Studio Code**: With "C#" Dev Kit extension.

---

## 2. Pre-Migration Checklist (Source Device)
Before transferring files, ensure you have a clean state or necessary backups:

1.  **Clean the Solution**: Remove build artifacts to reduce file size.
    ```powershell
    dotnet clean
    ```
    *   *Tip*: You can manually delete `bin` and `obj` folders in the project directory.

2.  **Database Backup (Optional)**:
    *   If you have important data in your local database that you want to keep, export it to a `.sql` script or create a `.bak` file.
    *   *Note*: The current project contains a `database.sql` file. simple schema migrations can handle the structure, but data must be moved manually if needed.

---

## 3. Transfer Process
1.  **Copy Files**: Transfer the entire **VerixSoftMvc** solution folder (containing `.sln` and the project subfolder) to the target device.
    *   *Method*: USB Drive, Cloud Storage (OneDrive/Google Drive), or Git (GitHub/GitLab).
2.  **Unzip/Place**: Extract the files to a known location (e.g., `C:\Projects\VerixSoftMvc`).
    *   *Warning*: Avoid placing it in deeply nested folders or folders with special characters to prevent path length issues.

---

## 4. Setup on Target Device

### Step 1: Restore Dependencies
Open a terminal in the project folder (where the `.sln` or `.csproj` file is) and run:
```powershell
dotnet restore
```
This downloads all necessary NuGet packages (Entity Framework, etc.).

### Step 2: Configure Database Connection
1.  Open `appsettings.json` in the `VerixSoftMvc` project folder.
2.  Locate the `"ConnectionStrings"` section.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VerixSoftMvcDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```
3.  **Critical Check**:
    *   If using **LocalDB** (VS Standard), keep `Server=(localdb)\\mssqllocaldb`.
    *   If using **SQL Server Express**, change to `Server=.\\SQLEXPRESS` or `Server=localhost`.
    *   If using **Docker**, update to `Server=localhost,1433;User Id=sa;Password=...`.

### Step 3: Initialize Database
You need to create the database schema on the new machine.

**Option A: Using Entity Framework (Recommended)**
1.  Ensure EF tools are installed:
    ```powershell
    dotnet tool install --global dotnet-ef
    ```
2.  Apply migrations:
    ```powershell
    dotnet ef database update
    ```
    *   *Result*: This creates the `VerixSoftMvcDb` database and creates all tables.

**Option B: Using SQL Script**
1.  Use the `database.sql` file provided in the root.
2.  Open SQL Server Management Studio (SSMS).
3.  Connect to your server.
4.  Run the script to create the tables.

### Step 4: Run the Application
```powershell
dotnet run
```
*   Access the site at the URL shown in the terminal (usually `http://localhost:5xxx`).

---

## 5. Troubleshooting Common Issues

| Issue | Cause | Solution |
| :--- | :--- | :--- |
| **"SQL Network Interfaces, error: 26"** | The code cannot find the SQL Server instance specified in `appsettings.json`. | 1. Check if SQL Server is running.<br>2. Verify `Server=...` in connection string matches your actual server name (use SSMS to check). |
| **"dotnet ef : The term 'dotnet' is not recognized"** | SDK not installed or not in PATH. | Install .NET 8 SDK and restart terminal. |
| **"HTTP Error 500.30 - ANCM In-Process Start Failure"** | Startup crash, often database related. | Run `dotnet run` in CLI to see the actual error exception details. |
| **Styling Missing (White page)** | Static files not loaded. | Ensure the `wwwroot` folder was copied successfully. |

## 6. Summary of Critical Care Points
*   **Connection Strings**: This is the #1 failure point. Always check `appsettings.json` matches the target machine's database setup.
*   **Data vs. Schema**: `dotnet ef database update` only moves the **Structure** (tables). It does NOT move your data (users, records) unless you have a Seed method in your code. If you need the old data, you must perform a SQL backup/restore.
