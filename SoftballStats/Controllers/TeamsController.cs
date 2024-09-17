using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Interfaces;
using Microsoft.AspNetCore.Identity;
using SoftballStats.ViewModels;

namespace SoftballStats.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeam _teamRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;

        public TeamsController (ITeam teamRepository, UserManager<User> userManager,
            IPhotoService photoService)
        {
            _teamRepository = teamRepository;
            _userManager = userManager;
            _photoService = photoService;
        } // end constructor
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<Team> teams = await _teamRepository.GetTeamsAsync(user.Id);
            return View("Index", teams);
        } // end index get

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(User);
            var teamVM = new AddTeamViewModel()
            {
                UserId = user.Id
            };
            return View(teamVM);
        } // end team Add get

        [HttpPost]
        public async Task<IActionResult> Add(AddTeamViewModel teamVM)
        {
            var result = await _photoService.AddPhotoAsync(teamVM.Image);
            if (ModelState.IsValid)
            {
                Team team = new Team
                {
                    TeamName = teamVM.TeamName,
                    UserID = teamVM.UserId,
                    Image = result.Url.ToString()
                };

                _teamRepository.Add(team);

                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);

            new AddTeamViewModel()
            {
                UserId = user.Id
            };

            return View(teamVM);
        } // end team Add post

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           var team = await _teamRepository.GetTeamAsync(id);
           return View(team);           
        } // end team Delete get

        [HttpPost]
        public async Task<IActionResult> Delete(Team team)
        {
            _teamRepository.Delete(team);
            return RedirectToAction("Index");
        } // end team Delete post

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Team team = await _teamRepository.GetTeamAsync(id);
            return View(team);
        } // end team Edit get

        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamRepository.Update(team);
                return RedirectToAction("Index");
            }
            return View(team);
        } // end team Edit post
    } // controller
} // end namespace