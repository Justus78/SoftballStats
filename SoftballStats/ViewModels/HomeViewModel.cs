using SoftballStats.Models;

namespace SoftballStats.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
