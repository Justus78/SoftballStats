using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.ViewModels;
using Microsoft.AspNetCore.Identity;

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
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // add the image to cloudinary
                var result = await _photoService.AddPhotoAsync(playerVM.Image);                

                // create a new player
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

                // add the player to the database
                _playerRepository.Add(player);

                // return to the index page
                return RedirectToAction("Index");
            }

            // if the model state is not valid, return to the view with the model
            var user = await _userManager.GetUserAsync(User);

            // add the user to the view model
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

            // return the view with the model
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
                Number = player.Number,
                Position = player.Position,
                Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id),
                UserId = player.UserID
            };

            return View(playerVM); // return the view with the new PlayerViewModel
        } // end player Edit get

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlayerViewModel playerVM, int selectedTeam)
        {
            // need to be able to edit without having to change
            // the profile image
            
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // check if the image is null
                if(playerVM.Image == null) // dont change profile pic
                {
                    // get the player to update to use the same image url
                    var playerToUpdate  = await _playerRepository.GetPlayerAsyncNoTracking(playerVM.PlayerID); 

                    // create the player to update
                    Player player = new Player
                    {
                        PlayerID = playerVM.PlayerID,
                        FirstName = playerVM.FirstName,
                        LastName = playerVM.LastName,
                        Number = playerVM.Number,
                        Position = playerVM.Position,
                        TeamID = selectedTeam,
                        UserID = playerVM.UserId,
                        Image = playerToUpdate.Image // keep the same image
                    };

                    _playerRepository.Update(player); // update the player

                    return RedirectToAction("Index"); // return to the index page
                }

                // change profile pic in cloudinary before updating the player
                var userPlayer = await _playerRepository.GetPlayerAsyncNoTracking(playerVM.PlayerID);

                // check if the userPlayer is not null
                if (userPlayer != null)
                {
                    // try to delete the current image from cloudinary for this player
                    try
                    {
                        // check if the image is not null
                        if (userPlayer.Image != null)
                        {
                            // delete the current image from cloudinary for this player
                            await _photoService.DeletePhotoAsync(userPlayer.Image);
                        }
                        
                    }
                    catch
                    {
                        // if error, return to the edit view with the clubVM
                        ModelState.AddModelError("", "Could not delete photo");

                        // get the user
                        var user = await _userManager.GetUserAsync(User);

                        // repopulate the teams with the user id
                        playerVM.Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id);

                        // return the view with the playerVM
                        return View(playerVM);
                    }

                    // add the new image to cloudinary
                    var result = await _photoService.AddPhotoAsync(playerVM.Image);

                    // create the player to update
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

                    // update the player
                    _playerRepository.Update(player);

                }

                // return to the index page
                return RedirectToAction("Index");
            }
            else
            {
                // get the user
                var user = await _userManager.GetUserAsync(User);

                // repopulate the teams with the user id
                playerVM.Teams = (List<Team>)await _teamRepository.GetTeamsAsync(user.Id);

                // return the view with the playerVM
                return View(playerVM);
            }
           
        } // end player Edit post

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // get the player witht the id
            var player = await _playerRepository.GetPlayerAsync(id);

            // return to the view with the player
            return View(player);
        } // end player Delete

        [HttpPost]
        public async Task<IActionResult> Delete(Player player)
        {
            // delete the player
            _playerRepository.Delete(player);

            // return to the index page
            return RedirectToAction("Index");
        } // end player Delete post
    } // controller
} // end namespace
