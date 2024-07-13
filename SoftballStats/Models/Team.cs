using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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
        
    } // end team model
} // end namespace
