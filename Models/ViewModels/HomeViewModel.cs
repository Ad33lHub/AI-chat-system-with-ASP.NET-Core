using VerixSoftMvc.Models;

namespace VerixSoftMvc.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Service> Services { get; set; } = new();
        public List<TechStack> TechStacks { get; set; } = new();
        public List<Stat> Stats { get; set; } = new();
        public List<Benefit> Benefits { get; set; } = new();
    }
}
