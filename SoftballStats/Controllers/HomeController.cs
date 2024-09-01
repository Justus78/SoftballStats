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
        private readonly UserManager<User> _userManager;
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        

        public HomeController(UserManager<User> userManager,
            IPlayer playerRepository, ITeam teamRepository)
        {
            _userManager = userManager;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        } // end logger

        public IActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    HomeViewModel homeViewModel = new HomeViewModel
                    {
                        Players = _playerRepository.GetPlayersAsync(_userManager.GetUserId(User)).Result,
                        Teams = _teamRepository.GetTeamsAsync(_userManager.GetUserId(User)).Result
                    };

                    if (homeViewModel.Players == null)
                    {
                        return RedirectToAction("Add", "Player");
                    }
                    if(homeViewModel.Teams == null)
                    {
                        return RedirectToAction("Add", "Team");
                    }

                    return View(homeViewModel);
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
