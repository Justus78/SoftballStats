using System.ComponentModel.DataAnnotations;

namespace SoftballStats.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        [Required]
        public string TeamName { get; set; }
        public ICollection<Player> Players { get; set; }
        
    } // end team model
} // end namespace
