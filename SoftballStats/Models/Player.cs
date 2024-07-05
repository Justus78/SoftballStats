using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftballStats.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Number { get; set; }
        public string? Position { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }
        [ValidateNever]
        public Team Team { get; set; }
       
    } // end player model
} // end namespace
