using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Repositories;
using SoftballStats.ViewModels;

namespace SoftballStats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly PlayerRepository _playerRepository;
        private readonly TeamRepository _teamRepository;
        

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
                    //HomeViewModel homeViewModel = new HomeViewModel
                    //{
                    //    Players = _playerRepository.GetPlayersAsync(_userManager.GetUserId(User)).Result,
                    //    Teams = _teamRepository.GetTeamsAsync(_userManager.GetUserId(User)).Result
                    //};

                    return View(/*homeViewModel*/);
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
