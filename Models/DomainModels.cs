using System.ComponentModel.DataAnnotations;

namespace VerixSoftMvc.Models
{
    public class Service
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string IconName { get; set; } = string.Empty;
    }

    public class TechStack
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class Stat
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }

    public class Benefit
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
