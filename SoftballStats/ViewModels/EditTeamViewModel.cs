namespace SoftballStats.ViewModels
{
    public class EditTeamViewModel
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public IFormFile? Image { get; set; }
        public string UserID { get; set; }
    }
}
