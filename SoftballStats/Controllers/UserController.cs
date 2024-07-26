using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SoftballStats.Models;
using SoftballStats.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SoftballStats.Controllers
{
    [Authorize("Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            foreach (User user in _userManager.Users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = _roleManager.Roles
            };

            return View(model);
        } // end Index

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                        TempData["Message"] = errorMessage;
                    } // end foreach
                } // end if               
            }// end if 
            return RedirectToAction("Index");
        } // end Delete

        // Add methods go here

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. Click 'Create Admin Role' to create it first.";
            }
            else
            {
                User user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            } // end if else
            return RedirectToAction("Index");
        } // end post add to admin

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        } // end post remove from admin

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        } // end post remove from admin

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        } // end post create admin role


    } // end controller
} // end namespace
