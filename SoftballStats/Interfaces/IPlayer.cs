using SoftballStats.Models;

namespace SoftballStats.Interfaces
{
    public interface IPlayer
    {
        Task<IEnumerable<Player>> GetPlayersAsync();
        Task<Player> GetPlayerAsync(int id);

        bool Add(Player player);
        bool Update(Player player);
        bool Delete(Player player);
        bool Save();
    }// end interface   
}// end namespace
