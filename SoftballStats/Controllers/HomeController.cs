using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;

namespace SoftballStats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        } // end logger

        public IActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return View("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            } catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }

        } // end index get

       
    } // end controller
} // end namespace
