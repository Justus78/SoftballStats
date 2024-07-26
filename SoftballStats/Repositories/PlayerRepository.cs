using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.Data;
using Microsoft.EntityFrameworkCore;

namespace SoftballStats.Repositories
{    
    public class PlayerRepository : IPlayer
    {
        // member variable to hold the context
        private readonly StatContext _context;

        // add the contect in the constructor
        public PlayerRepository(StatContext context)
        {
            _context = context;
        } // end constructor

        public async Task<IEnumerable<Player>> GetPlayersAsync(string id)
        {
            return await _context.Players.Where(u => u.UserID == id).ToListAsync();
        }

        public async Task<Player> GetPlayerAsync(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerID == id);
        }

        public bool Add(Player player)
        {
            _context.Players.Add(player);
            return Save();
        }

        public bool Update(Player player)
        {
            _context.Players.Update(player);
            return Save();
        }

        public bool Delete(Player player)
        {            
            if (player != null) { _context.Remove(player); }
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    } // end repository
} // end namespace
