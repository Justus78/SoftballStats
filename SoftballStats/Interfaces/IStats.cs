using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface IStats
    {
        Task<IEnumerable<GameStats>> GetStatsAsync(int id);
        Task<GameStats> GetStatAsync(int id);

        bool Add(GameStats stats);
        bool Update(GameStats stats);
        bool Delete(GameStats stat);
        bool Save();
    } // end interface
}// end namespace
