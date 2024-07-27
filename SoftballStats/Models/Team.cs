using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftballStats.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        [Required]
        public string TeamName { get; set; }
        [ValidateNever]
        public ICollection<Player>? Players { get; set; }

        [ValidateNever]
        public User? User { get; set; }
        [ForeignKey("User")]
        public string? UserID { get; set; }

    } // end team model
} // end namespace
