using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface ITeam
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamAsync(int id);

        bool Add(Team team);
        bool Update(Team team);
        bool Delete(int id);
        bool Save();
    } // end interface  
} // end namespace
