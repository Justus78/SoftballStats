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