# VerixSoft - Installation & Setup Guide

## ?? Prerequisites

- **.NET 10 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server** or **LocalDB**
- **Node.js** (optional, if using npm packages)

## ?? Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/verixsoft-mvc.git
cd VerixSoftMvc
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Update Database Connection

Edit `appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=VerixSoftDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

For SQL Azure:
```json
"DefaultConnection": "Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=VerixSoftDb;Persist Security Info=False;User ID=admin;Password=YourPassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

### 4. Run Database Migrations

```bash
dotnet ef database update
```

This will:
- Create the database
- Create all required tables
- Seed initial data

### 5. Run the Application

```bash
dotnet run
```

The application will be available at:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

## ??? Project Structure

```
VerixSoftMvc/
??? Controllers/           # MVC Controllers
?   ??? HomeController.cs
??? Models/               # Domain Models & ViewModels
?   ??? DomainModels.cs
?   ??? ViewModels/
?       ??? HomeViewModel.cs
??? Views/               # Razor Views
?   ??? Home/
?   ?   ??? Index.cshtml      # Home page
?   ?   ??? About.cshtml      # About page
?   ?   ??? Privacy.cshtml    # Privacy policy
?   ??? Shared/
?       ??? _Layout.cshtml    # Master layout
?       ??? Error.cshtml      # Error page
??? Data/                # Database Context
?   ??? ApplicationDbContext.cs
??? wwwroot/            # Static Files
?   ??? css/
?   ?   ??? site.css    # Main stylesheet
?   ??? js/
?   ??? images/
??? Properties/         # Project Properties
??? Migrations/         # EF Core Migrations
??? Program.cs          # Application Entry Point
??? appsettings.json    # Configuration
??? DESIGN_GUIDE.md     # Design Documentation
```

## ?? Design Files

All design and styling information is documented in:
- **`DESIGN_GUIDE.md`** - Complete design system documentation
- **`wwwroot/css/site.css`** - All CSS styles and variables

## ??? Database

### Entities

The application uses the following database entities:

1. **Service**
   - Id (int)
   - Title (string)
   - Description (string)
   - IconName (string)

2. **TechStack**
   - Id (int)
   - Name (string)
   - LogoUrl (string)
   - Category (string)

3. **Stat**
   - Id (int)
   - Number (string)
   - Label (string)

4. **Benefit**
   - Id (int)
   - Text (string)

### Seeding Data

To add initial data, modify the `OnModelCreating` method in `ApplicationDbContext.cs`:

```csharp
modelBuilder.Entity<Service>().HasData(
    new Service 
    { 
        Id = 1, 
        Title = "Web Development",
        Description = "Modern, responsive websites...",
        IconName = "globe"
    }
    // Add more services
);
```

Then run migration:
```bash
dotnet ef migrations add SeedInitialData
dotnet ef database update
```

## ?? Configuration

### Environment Variables

Create a `.env` file in the project root:

```env
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=YourConnectionString
```

### App Settings

Edit `appsettings.json` for development and `appsettings.Production.json` for production:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=VerixSoftDb;Trusted_Connection=true;"
  }
}
```

## ?? Adding Dependencies

### Install NuGet Package

```bash
dotnet add package PackageName
```

### Update All Packages

```bash
dotnet package update
```

## ?? Testing

### Run Tests

```bash
dotnet test
```

### Build for Testing

```bash
dotnet build -c Release
```

## ?? Responsive Design Testing

Test on different screen sizes:
- **Desktop**: 1920x1080, 1366x768, 1024x768
- **Tablet**: 768x1024, 768x768
- **Mobile**: 390x844, 375x667, 320x568

Use browser DevTools F12 ? Toggle Device Toolbar

## ?? Deployment

### Publishing

#### Self-Hosted

```bash
dotnet publish -c Release -o ./publish
```

#### Docker

Create `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "VerixSoftMvc.dll"]
```

Build and run:
```bash
docker build -t verixsoft:latest .
docker run -p 80:80 verixsoft:latest
```

#### Azure App Service

```bash
az webapp up --name verixsoft --resource-group myresourcegroup --runtime "dotnet:10"
```

### Production Checklist

- [ ] Update connection strings
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Enable HTTPS enforcement
- [ ] Configure error handling
- [ ] Set up logging
- [ ] Add authentication if needed
- [ ] Configure CORS if needed
- [ ] Set up database backups
- [ ] Configure SSL certificates
- [ ] Enable caching headers
- [ ] Minify CSS and JS
- [ ] Compress images

## ?? Security

### HTTPS Enforcement

```csharp
app.UseHttpsRedirection();
```

### HSTS (HTTP Strict Transport Security)

```csharp
app.UseHsts();
```

### Content Security Policy

Add to response headers:
```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", 
        "default-src 'self'; style-src 'self' 'unsafe-inline' https://fonts.googleapis.com");
    await next();
});
```

## ?? Troubleshooting

### Port Already in Use

```bash
# Find process using port 5000
netstat -ano | findstr :5000
# Kill process
taskkill /PID <PID> /F
```

### Database Connection Error

```bash
# Reset LocalDB
sqllocaldb reset mssqllocaldb
sqllocaldb start mssqllocaldb
```

### Migration Issues

```bash
# Remove last migration
dotnet ef migrations remove
# Remove all migrations
dotnet ef database drop -f
dotnet ef database update
```

### Clear Cache

```bash
# Clear build artifacts
dotnet clean
# Clear NuGet cache
dotnet nuget locals all --clear
```

## ?? Useful Commands

```bash
# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove migration
dotnet ef migrations remove

# View migration SQL
dotnet ef migrations script

# Check project dependencies
dotnet list package

# Format code
dotnet format

# Run in watch mode (auto-reload)
dotnet watch run

# Build in Release mode
dotnet build -c Release

# Get project information
dotnet project-info
```

## ?? Support

For issues or questions:
- Check the [Design Guide](DESIGN_GUIDE.md)
- Review [README.md](README.md)
- Create an issue on GitHub
- Email: support@verixsoft.com

## ?? License

This project is licensed under the MIT License - see LICENSE.md for details

## ? Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

**Last Updated**: January 2025  
**Maintained by**: VerixSoft Development Team
