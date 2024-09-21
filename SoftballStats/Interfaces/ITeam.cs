using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface ITeam
    {
        Task<IEnumerable<Team>> GetTeamsAsync(string id);
        Task<Team> GetTeamAsync(int id);
        Task<Team> GetTeamAsyncNoTracking(int id);

        bool Add(Team team);
        bool Update(Team team);
        bool Delete(Team team);
        bool Save();
    } // end interface  
} // end namespace
