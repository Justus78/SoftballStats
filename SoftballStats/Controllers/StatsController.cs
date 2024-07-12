using Microsoft.AspNetCore.Mvc;
using SoftballStats.Interfaces;
using SoftballStats.ViewModels;
using SoftballStats.Models; 

namespace SoftballStats.Controllers
{
    public class StatsController : Controller
    {
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        private readonly IStats _statRepository;
        public StatsController(IPlayer playerRepository, ITeam teamRepository, IStats statRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _statRepository = statRepository;
        } // end constructor
        public async Task<IActionResult> PlayerStats(int id)
        {
            PlayerStatsViewModel playerStats = new PlayerStatsViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(id),
                Stats = await _statRepository.GetStatsAsync(id)
                
            };
            
            return View(playerStats);
        } // end playerstats

        [HttpGet]
        public async Task<IActionResult> AddStats(int id)
        {            
            AddStatViewModel addStatViewModel = new AddStatViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(id),
                Stats = new GameStats()
            };
            return View(addStatViewModel);
        } // end stat Add get

        [HttpPost]
        public async Task<IActionResult> AddStats(AddStatViewModel newStats)
        {
            if (ModelState.IsValid)
            {
                GameStats gameStats = new GameStats
                {
                    PlayerID = newStats.Player.PlayerID,
                    Opponent = newStats.Stats.Opponent,
                    GameDate = newStats.Stats.GameDate,
                    AtBats = newStats.Stats.AtBats,
                    Hits = newStats.Stats.Hits,
                    Runs = newStats.Stats.Runs,
                    RBIs = newStats.Stats.RBIs,
                    Walks = newStats.Stats.Walks,
                    Strikeouts = newStats.Stats.Strikeouts,
                    StolenBases = newStats.Stats.StolenBases,
                    Errors = newStats.Stats.Errors,
                    Singles = newStats.Stats.Singles,
                    HomeRuns = newStats.Stats.HomeRuns,
                    Doubles = newStats.Stats.Doubles,
                    Triples = newStats.Stats.Triples,
                    HitByPitch = newStats.Stats.HitByPitch,
                    SacrificeFly = newStats.Stats.SacrificeFly,
                    SacrificeBunt = newStats.Stats.SacrificeBunt
                };

                _statRepository.Add(gameStats);
                return RedirectToAction("PlayerStats", new { id = newStats.Player.PlayerID});
            }
            AddStatViewModel addStatViewModel = new AddStatViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(newStats.Player.PlayerID),
                Stats = newStats.Stats
            };
            return View(addStatViewModel);
        } // end stat Add post
    } // controller
} // end namespace
