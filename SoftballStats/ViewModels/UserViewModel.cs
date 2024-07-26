using Microsoft.AspNetCore.Identity;
using SoftballStats.Models; 
namespace SoftballStats.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
