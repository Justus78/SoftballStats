using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Repositories;
using SoftballStats.ViewModels;
using SoftballStats.Interfaces;

namespace SoftballStats.Controllers
{
    public class HomeController : Controller
    {
        // member variables for Dependency Injection
        private readonly UserManager<User> _userManager;
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        

        // constructor to implement Dependency Injection
        public HomeController(UserManager<User> userManager,
            IPlayer playerRepository, ITeam teamRepository)
        {
            _userManager = userManager;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        } // end logger


        public IActionResult Index()
        {
            // try to get the players and teams for the user
            try
            {
                // check if the user is authenticated
                if (User.Identity.IsAuthenticated)
                {
                    // create a new home view model
                    HomeViewModel homeViewModel = new HomeViewModel
                    {
                        // get the players and teams for the user
                        Players = _playerRepository.GetPlayersAsync(_userManager.GetUserId(User)).Result,
                        Teams = _teamRepository.GetTeamsAsync(_userManager.GetUserId(User)).Result
                    };

                    // check if the players and teams are null
                    if (homeViewModel.Players == null)
                    {
                        return RedirectToAction("Add", "Player");
                    }

                    if (homeViewModel.Teams == null)
                    {
                        return RedirectToAction("Add", "Teams");
                    }

                    // return the view with the home view model
                    return View(homeViewModel);
                }
                else
                {
                    // if the user is not authenticated return to the login page
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                // if there is an exception return to the login page
                return RedirectToAction("Login", "Account");
            }

        } // end index get

       
    } // end controller
} // end namespace
