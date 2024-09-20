using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SoftballStats.Models;

namespace SoftballStats.ViewModels
{
    public class EditPlayerViewModel
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? Image { get; set; }
        public int Number { get; set; }
        public string? Position { get; set; }
        [ValidateNever]
        public List<Team>? Teams { get; set; }
        public int TeamID { get; set; }
        public string UserId { get; set; }
    }
}
