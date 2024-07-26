using SoftballStats.Models;
using SoftballStats.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SoftballStats.ViewModels
{
    public class PlayerViewModel
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public string? Position { get; set; }
        [ValidateNever]
        public List<Team> Teams { get; set; }
        public int TeamID { get; set; }
        public string UserId { get; set; }
    } // end view model 
} // end namespace

