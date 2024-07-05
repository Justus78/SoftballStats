using Microsoft.AspNetCore.Mvc;
using SoftballStats.Repositories;
using SoftballStats.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SoftballStats.Interfaces;

namespace SoftballStats.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeam _teamRepository;

        public TeamsController (ITeam teamRepository)
        {
            _teamRepository = teamRepository;
        } // end constructor
        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamRepository.GetTeamsAsync();
            return View("Index", teams);
        } // end index get

        [HttpGet]
        public async Task<IActionResult> Add()
        {
           var team = new Team();
            return View(team);
        } // end team Add get

        [HttpPost]
        public async Task<IActionResult> Add(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamRepository.Add(team);
                return RedirectToAction("Index");
            }
            return View(team);
        } // end team Add post
    } // controller
} // end namespace