﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string? Image { get; set; }   
        [Required]
        public int Number { get; set; }
        public string? Position { get; set; }
        [ForeignKey("Team")]
        public int? TeamID { get; set; }
        [ValidateNever]
        public Team? Team { get; set; }
        [ValidateNever]
        public User? User { get; set; }
        [ForeignKey("User")]
        public string? UserID { get; set; }

        public ICollection<GameStats> GameStats { get; set; } = new List<GameStats>();


    } // end player model
} // end namespace
