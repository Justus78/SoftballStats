using SoftballStats.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SoftballStats.ViewModels
{
    public class PlayerViewModel
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public int Number { get; set; }
        public string? Position { get; set; }
        [ValidateNever]
        public List<Team>? Teams { get; set; } = null;
        public int TeamID { get; set; }
        public string UserId { get; set; }
    } // end view model 
} // end namespace

