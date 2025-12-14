using Microsoft.EntityFrameworkCore;
using VerixSoftMvc.Models;

namespace VerixSoftMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<TechStack> TechStacks { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<Benefit> Benefits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed Data
            modelBuilder.Entity<Stat>().HasData(
                new Stat { Id = 1, Number = "100+", Label = "Projects Delivered" },
                new Stat { Id = 2, Number = "80+", Label = "Happy Clients" },
                new Stat { Id = 3, Number = "10+", Label = "Team Members" },
                new Stat { Id = 4, Number = "2+", Label = "Years Experience" }
            );

            modelBuilder.Entity<Benefit>().HasData(
                new Benefit { Id = 1, Text = "Agile Development Methodology" },
                new Benefit { Id = 2, Text = "24/7 Technical Support" },
                new Benefit { Id = 3, Text = "Transparent Communication" },
                new Benefit { Id = 4, Text = "On-time Delivery Guarantee" },
                new Benefit { Id = 5, Text = "Quality Assurance" },
                new Benefit { Id = 6, Text = "Scalable Solutions" }
            );
            
             modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Title = "Web Development", Description = "Custom web applications built with cutting-edge technologies for optimal performance and scalability.", IconName = "Code2" },
                new Service { Id = 2, Title = "Mobile Apps", Description = "Native and cross-platform mobile solutions that deliver exceptional user experiences.", IconName = "Smartphone" },
                new Service { Id = 3, Title = "Cloud Solutions", Description = "Scalable cloud infrastructure and migration services for modern businesses.", IconName = "Cloud" },
                new Service { Id = 4, Title = "AI/ML Integration", Description = "Intelligent automation and machine learning solutions to drive innovation.", IconName = "Brain" }
            );

            modelBuilder.Entity<TechStack>().HasData(
                new TechStack { Id = 1, Name = "React", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg", Category = "Frontend" },
                new TechStack { Id = 2, Name = "Next.js", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nextjs/nextjs-original.svg", Category = "Frontend" },
                new TechStack { Id = 3, Name = "TypeScript", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg", Category = "Frontend" },
                new TechStack { Id = 4, Name = "Node.js", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nodejs/nodejs-original.svg", Category = "Backend" },
                new TechStack { Id = 5, Name = "ASP.NET Core", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg", Category = "Backend" },
                new TechStack { Id = 6, Name = "SQL Server", LogoUrl = "https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg", Category = "Database" }
                // Added a subset for brevity in seed, can add more if needed
            );
        }
    }
}
