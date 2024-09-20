using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.ViewModels;

namespace SoftballStats.Controllers
{
    public class AccountController : Controller
    {
        // member variables for Dependency Injection
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        // constructor to implement Dependency Injection
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register() // return the register view
        {
            return View();
        } // end get register

        // post register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // validate model state
            if (ModelState.IsValid)
            {
                // create a new user
                User user = new User
                {
                    UserName = model.UserName // set the username
                };

                // create the user
                var result = _userManager.CreateAsync(user, model.Password).Result;

                // check if the user was created
                if (result.Succeeded)
                {
                    // sign in the user
                    await _signInManager.SignInAsync(user, false);

                    // redirect to the home page
                    return RedirectToAction("Index", "Home");
                }
                 // if user was not created add the errors to the model state
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    } // end foreach
                } // end if else
            } // end if for model state            

            // return the view with the model
            return View(model);
        } // end post register



        public IActionResult Login(string returnUrl)
        {
            // create a new login view model
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            // check if there is a success message
            if (TempData["Success"] != null)
            {
                ViewBag.SuccessMessage = TempData["Success"].ToString();
            }

            // return the view with the model
            return View(model);
        } // end login get

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // sign in the user
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                // check if the user was signed in
                if (result.Succeeded)
                {
                    // check if there is a return url
                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        // return the user to the return url
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        // return the user to the home page
                        return RedirectToAction("Index", "Home");
                    } // end if else
                } // end if succeeded
            } // end if modelstate

            // add an error to the model state
            ModelState.AddModelError("", "Invalid Username or Password");

            // return the view with the model
            return View(model);
        } // end login post



        public async Task<IActionResult> Logout()
        {
            // sign out the user
            await _signInManager.SignOutAsync();

            // return the user to the home page
            return RedirectToAction("Index", "Home");
        } // end logout


    } // end controller
} // end namespace
