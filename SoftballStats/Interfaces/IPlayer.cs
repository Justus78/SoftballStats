using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface IPlayer
    {
        Task<IEnumerable<Player>> GetPlayersAsync(string id);
        Task<Player> GetPlayerAsync(int id);
        Task<Player> GetPlayerAsyncNoTracking(int id);

        bool Add(Player player);
        bool Update(Player player);
        bool Delete(Player player);
        bool Save();
    }// end interface   
}// end namespace
