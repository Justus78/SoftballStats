using Microsoft.AspNetCore.Mvc;
using SoftballStats.Models;
using SoftballStats.Interfaces;
using Microsoft.AspNetCore.Identity;
using SoftballStats.ViewModels;
using CloudinaryDotNet.Actions;

namespace SoftballStats.Controllers
{
    public class TeamsController : Controller
    {
        // member variables for Dependency Injection
        private readonly ITeam _teamRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;

        // constructor to implement Dependency Injection
        public TeamsController(ITeam teamRepository, UserManager<User> userManager,
            IPhotoService photoService)
        {
            _teamRepository = teamRepository;
            _userManager = userManager;
            _photoService = photoService;
        } // end constructor

        public async Task<IActionResult> Index()
        {
            // get the user
            var user = await _userManager.GetUserAsync(User);

            // get the teams for the user
            IEnumerable<Team> teams = await _teamRepository.GetTeamsAsync(user.Id);

            // return the view with the teams
            return View("Index", teams);
        } // end index get

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get the user
            var user = await _userManager.GetUserAsync(User);

            // new teamviewmodel with the user
            var teamVM = new AddTeamViewModel()
            {
                UserId = user.Id
            };

            // return the view with the teamviewmodel
            return View(teamVM);
        } // end team Add get

        [HttpPost]
        public async Task<IActionResult> Add(AddTeamViewModel teamVM)
        {            
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // add the image to cloudinary
                var result = await _photoService.AddPhotoAsync(teamVM.Image);

                // create a new team with the cloudinary result url
                Team team = new Team
                {
                    TeamName = teamVM.TeamName,
                    UserID = teamVM.UserId,
                    Image = result.Url.ToString()
                };

                // add the team to the database
                _teamRepository.Add(team);

                // return to the index view
                return RedirectToAction("Index");
            }

            // if model state is not valid, return to the view with the teamviewmodel

            // get the user
            var user = await _userManager.GetUserAsync(User);

            // new teamviewmodel with the user
            new AddTeamViewModel()
            {
                UserId = user.Id
            };

            // return the view with the teamviewmodel
            return View(teamVM);
        } // end team Add post

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // get the team with the id
            var team = await _teamRepository.GetTeamAsync(id);

            // return the view with the team
            return View(team);
        } // end team Delete get

        [HttpPost]
        public async Task<IActionResult> Delete(Team team)
        {
            // delete the team
            _teamRepository.Delete(team);

            // return to the index view
            return RedirectToAction("Index");
        } // end team Delete post

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // get the team with the id
            Team team = await _teamRepository.GetTeamAsync(id);

            // new editteamviewmodel with the team and user
            EditTeamViewModel teamVM = new EditTeamViewModel
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                UserID = team.UserID
            };

            // return the view with the editteamviewmodel
            return View(teamVM);
        } // end team Edit get

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel editVM)
        {
            // check if the model state is valid
            if (ModelState.IsValid)
            {
                // check if the image is null
                if (editVM.Image == null)
                {
                    // get the team to update with the id from the DB
                    var teamToUpdate = await _teamRepository.GetTeamAsyncNoTracking(editVM.TeamID);

                    // create a new team with the image from the team to update
                    Team team = new Team
                    {
                        TeamID = editVM.TeamID,
                        TeamName = editVM.TeamName,
                        Image = teamToUpdate.Image,
                        UserID = editVM.UserID
                    };

                    // update the team
                    _teamRepository.Update(team);

                    // redirect to index
                    return RedirectToAction("Index");
                } 
                else // if the image is not null
                {
                    // get the team with the id from the DB
                    var userTeam = await _teamRepository.GetTeamAsyncNoTracking(editVM.TeamID);

                    // check if the team is not null
                    if (userTeam != null)
                    {
                        // check if the image is not null
                        if (editVM.Image != null)
                        {
                            // delete the current image from cloudinary for this team
                            try
                            {
                                // delete the current image from cloudinary for this team
                                await _photoService.DeletePhotoAsync(userTeam.Image);
                            }
                            catch
                            {
                                // if error, return to the edit view with the clubVM
                                ModelState.AddModelError("", "Could not delete photo");
                                
                                // return the view with the editVM
                                return View(editVM);
                            }
                        }
                        
                        // add the new photo to cloudinary
                        var result = await _photoService.AddPhotoAsync(editVM.Image);
                        
                        // create a new team with the cloudinary result url
                        Team team = new Team
                        {
                            TeamID = editVM.TeamID,
                            TeamName = editVM.TeamName,
                            Image = result.Url.ToString(),
                            UserID = editVM.UserID
                        };
                        
                        // update the team
                        _teamRepository.Update(team);
                        
                        // redirect to index
                        return RedirectToAction("Index");
                    }
                }
            } // end if modelstate is valid

            // if model state is not valid, return to the edit view with the editTeamViewModel
            EditTeamViewModel editTeamViewModel = new EditTeamViewModel
            {
                TeamID = editVM.TeamID,
                TeamName = editVM.TeamName,
                Image = editVM.Image,
                UserID = editVM.UserID
            };

            // return the view with the editTeamViewModel
            return View(editTeamViewModel);            
        } // end team Edit post
    } // controller
} // end namespace
