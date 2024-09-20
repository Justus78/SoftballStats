using Microsoft.AspNetCore.Mvc;
using SoftballStats.Interfaces;
using SoftballStats.ViewModels;
using SoftballStats.Models; 

namespace SoftballStats.Controllers
{
    public class StatsController : Controller
    {

        // member variables for Dependency Injection
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        private readonly IStats _statRepository;

        // constructor to implement Dependency Injection
        public StatsController(IPlayer playerRepository, ITeam teamRepository, IStats statRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _statRepository = statRepository;
        } // end constructor
        public async Task<IActionResult> PlayerStats(int id)
        {
            // new playerstats view model with player and stats
            PlayerStatsViewModel playerStats = new PlayerStatsViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(id), // get the player
                Stats = await _statRepository.GetStatsAsync(id) // get the stats
                
            };
            
            // return the view with the playerstats view model
            return View(playerStats);
        } // end playerstats

        [HttpGet]
        public async Task<IActionResult> AddStats(int id)
        {          
            // new addstat view model with player and stats
            AddStatViewModel addStatViewModel = new AddStatViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(id), // get the player
                Stats = new GameStats() // new gamestats
            };

            // return the view with the addstat view model
            return View(addStatViewModel);
        } // end stat Add get

        [HttpPost]
        public async Task<IActionResult> AddStats(AddStatViewModel newStats)
        {
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // create a new gamestats
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

                // add the gamestats
                _statRepository.Add(gameStats);

                // return to the playerstats view
                return RedirectToAction("PlayerStats", new { id = newStats.Player.PlayerID});
            }

            // if model state is not valid, return to the view with the model
            AddStatViewModel addStatViewModel = new AddStatViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(newStats.Player.PlayerID), // get the player
                Stats = newStats.Stats // get the stats
            };

            // return the view with the model
            return View(addStatViewModel);
        } // end stat Add post

        public async Task<IActionResult> Games(int id)
        {
            // new playerstats view model with player and stats
            PlayerStatsViewModel games = new PlayerStatsViewModel()
            {
                Player = await _playerRepository.GetPlayerAsync(id), // get the player
                Stats = await _statRepository.GetStatsAsync(id) // get the stats
            };

            // return the view with the playerstats view model
            return View(games);
        } // end games

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // get the gamestats with the id
            GameStats gameStats = await _statRepository.GetStatAsync(id);

            // return the view with the gamestats
            return View(gameStats);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameStats stat)
        {
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // update the gamestats
                _statRepository.Update(stat);

                // return to the games view
                return RedirectToAction("Games", new { id = stat.PlayerID});
            }

            // if model state is not valid, return to the view with the model
            return View(stat);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // get the gamestats with the id
            GameStats gameStats = await _statRepository.GetStatAsync(id);

            // return the view with the gamestats
            return View(gameStats);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GameStats stat)
        {
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // delete the gamestats
                _statRepository.Delete(stat);

                // return to the games view
                return RedirectToAction("Games", new { id = stat.PlayerID });
            }

            // if model state is not valid, return to the view with the model
            return View(stat);
        }
    } // controller
} // end namespace
