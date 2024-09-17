namespace SoftballStats.ViewModels
{
    public class AddTeamViewModel
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public IFormFile Image { get; set; }
        public string UserId { get; set; }

    } // end team view model
} // end namespace
