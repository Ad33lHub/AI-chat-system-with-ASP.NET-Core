using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VerixSoftMvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechStacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechStacks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Benefits",
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { 1, "Agile Development Methodology" },
                    { 2, "24/7 Technical Support" },
                    { 3, "Transparent Communication" },
                    { 4, "On-time Delivery Guarantee" },
                    { 5, "Quality Assurance" },
                    { 6, "Scalable Solutions" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "IconName", "Title" },
                values: new object[,]
                {
                    { 1, "Custom web applications built with cutting-edge technologies for optimal performance and scalability.", "Code2", "Web Development" },
                    { 2, "Native and cross-platform mobile solutions that deliver exceptional user experiences.", "Smartphone", "Mobile Apps" },
                    { 3, "Scalable cloud infrastructure and migration services for modern businesses.", "Cloud", "Cloud Solutions" },
                    { 4, "Intelligent automation and machine learning solutions to drive innovation.", "Brain", "AI/ML Integration" }
                });

            migrationBuilder.InsertData(
                table: "Stats",
                columns: new[] { "Id", "Label", "Number" },
                values: new object[,]
                {
                    { 1, "Projects Delivered", "100+" },
                    { 2, "Happy Clients", "80+" },
                    { 3, "Team Members", "10+" },
                    { 4, "Years Experience", "2+" }
                });

            migrationBuilder.InsertData(
                table: "TechStacks",
                columns: new[] { "Id", "Category", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Frontend", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg", "React" },
                    { 2, "Frontend", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nextjs/nextjs-original.svg", "Next.js" },
                    { 3, "Frontend", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg", "TypeScript" },
                    { 4, "Backend", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nodejs/nodejs-original.svg", "Node.js" },
                    { 5, "Backend", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg", "ASP.NET Core" },
                    { 6, "Database", "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg", "SQL Server" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "TechStacks");
        }
    }
}
