using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SoftballStats.Models;
using SoftballStats.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SoftballStats.Controllers
{
    // controller for the user
    [Authorize("Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        // member variables for Dependency Injection
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        // constructor to implement Dependency Injection
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // get the users
            List<User> users = new List<User>();

            // loop through the users and get the roles
            foreach (User user in _userManager.Users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }

            // create a new user view model
            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = _roleManager.Roles
            };

            // return the view with the user view model
            return View(model);
        } // end Index

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            // get the user
            User user = await _userManager.FindByIdAsync(id);

            // check if the user is not null
            if (user != null)
            {
                // delete the user
                IdentityResult result = await _userManager.DeleteAsync(user);

                // check if the result succeeded
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
            // get the admin role
            IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");

            // check if the admin role is null
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. Click 'Create Admin Role' to create it first.";
            }
            else
            {
                // get the user
                User user = await _userManager.FindByIdAsync(id);

                // add the user to the admin role
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            } // end if else
            return RedirectToAction("Index");
        } // end post add to admin

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {

            // get the user
            User user = await _userManager.FindByIdAsync(id);

            // remove the user from the admin role
            await _userManager.RemoveFromRoleAsync(user, "Admin");

            // return to the index view
            return RedirectToAction("Index");
        } // end post remove from admin

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            // get the role
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            // delete the role
            await _roleManager.DeleteAsync(role);

            // return to the index view
            return RedirectToAction("Index");
        } // end post remove from admin

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            // create the admin role
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // return to the index view
            return RedirectToAction("Index");
        } // end post create admin role


    } // end controller
} // end namespace
