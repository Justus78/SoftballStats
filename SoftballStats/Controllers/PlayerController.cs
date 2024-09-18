using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.ViewModels;
using Microsoft.AspNetCore.Identity;
using CloudinaryDotNet.Actions;

namespace SoftballStats.Controllers
{
    public class PlayerController : Controller
    {
        // member variables for DI
        private readonly IPlayer _playerRepository;
        private readonly ITeam _teamRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;       
        
        
        // constructor to implement DI
        public PlayerController(IPlayer playerRepository, ITeam teamRepository, 
            UserManager<User> userManager, IPhotoService photoService)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _photoService = photoService;
            _userManager = userManager;

        } // end constructor
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // get the user

            IEnumerable<Player> players = await _playerRepository.GetPlayersAsync(user.Id);// get the players for the user

            return View(players); // return the view with the players for the specific user
        } // end index get

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(User); // get the user

            var player = new PlayerViewModel() 
            { 
                Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id),  // get the teamd for the user
                UserId = user.Id // add the user to the viewmodel
            };
            return View(player);
        } // end player Add get

        [HttpPost]
        public async Task<IActionResult> Add(PlayerViewModel playerVM, int? selectedTeam)
        {
            if (ModelState.IsValid)
            {

                
                var result = await _photoService.AddPhotoAsync(playerVM.Image);                

                Player player = new Player
                {
                    PlayerID = playerVM.PlayerID,
                    FirstName = playerVM.FirstName,
                    LastName = playerVM.LastName,
                    Image = result.Url.ToString(),
                    Number = playerVM.Number,
                    Position = playerVM.Position,
                    TeamID = selectedTeam ?? null,
                    UserID = playerVM.UserId
                };
                _playerRepository.Add(player);

                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);

            playerVM = new PlayerViewModel
            {
                PlayerID = playerVM.PlayerID,
                FirstName = playerVM.FirstName,
                LastName = playerVM.LastName,
                Number = playerVM.Number,
                Position = playerVM.Position,
                Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id),
                UserId = playerVM.UserId
            };
            return View(playerVM);
        } // end player Add post

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Player player = await _playerRepository.GetPlayerAsync(id); // get the player

            var user = await _userManager.GetUserAsync(User); // get the user
            
            var playerVM = new EditPlayerViewModel // new PlayerViewModel
            {
                PlayerID = player.PlayerID,
                FirstName = player.FirstName,
                LastName = player.LastName,
                URL = player.Image,
                Number = player.Number,
                Position = player.Position,
                Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id),
                UserId = player.UserID
            };

            return View(playerVM); // return the view with the new PlayerViewModel
        } // end player Edit get

        [HttpPost]
        public async Task<IActionResult> Edit(PlayerViewModel playerVM, int selectedTeam)
        {
            
            if (ModelState.IsValid)
            {
                var userPlayer = await _playerRepository.GetPlayerAsyncNoTracking(playerVM.PlayerID);

                if (userPlayer != null)
                {
                    try
                    {
                        // delete the current image from cloudinary for this player
                        await _photoService.DeletePhotoAsync(userPlayer.Image);
                    }
                    catch
                    {
                        // if error, return to the edit view with the playerVM
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(playerVM);
                    }

                    var result = await _photoService.AddPhotoAsync(playerVM.Image);

                    Player player = new Player
                    {
                        PlayerID = playerVM.PlayerID,
                        FirstName = playerVM.FirstName,
                        LastName = playerVM.LastName,
                        Image = result.Url.ToString(),
                        Number = playerVM.Number,
                        Position = playerVM.Position,
                        TeamID = selectedTeam,
                        UserID = playerVM.UserId
                    };

                    _playerRepository.Update(player);

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(playerVM);
            }
           
        } // end player Edit post

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerRepository.GetPlayerAsync(id);
            return View(player);
        } // end player Delete

        [HttpPost]
        public async Task<IActionResult> Delete(Player player)
        {
            _playerRepository.Delete(player);
            return RedirectToAction("Index");
        } // end player Delete post
    } // controller
} // end namespace
