using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface IStats
    {
        Task<IEnumerable<Stats>> GetStatsAsync();
        Task<Stats> GetStatAsync(int id);

        bool Add(Stats stats);
        bool Update(Stats stats);
        bool Delete(Stats stat);
        bool Save();
    } // end interface
}// end namespace
