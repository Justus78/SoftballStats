﻿using Microsoft.AspNetCore.Mvc;
using SoftballStats.Repositories;
using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.ViewModels;

namespace SoftballStats.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        
        public PlayerController(IPlayer playerRepository, ITeam teamRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        } // end constructor
        public async Task<IActionResult> Index()
        {
            IEnumerable<Player> players = await _playerRepository.GetPlayersAsync();
            return View(players);
        } // end index get

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var player = new PlayerViewModel() { Teams = (List<Team>)await _teamRepository.GetTeamsAsync() };
            return View(player);
        } // end player Add get

        [HttpPost]
        public async Task<IActionResult> Add(PlayerViewModel playerVM, int selectedTeam)
        {
            if (ModelState.IsValid)
            {
                Player player = new Player
                {
                    PlayerID = playerVM.PlayerID,
                    FirstName = playerVM.FirstName,
                    LastName = playerVM.LastName,
                    Number = playerVM.Number,
                    Position = playerVM.Position,      
                    TeamID = selectedTeam
                };
                _playerRepository.Add(player);

                return RedirectToAction("Index");
            }
            return View(playerVM);
        } // end player Add post
    } // controller
} // end namespace
