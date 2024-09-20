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
            // get the players for the user, include the gamestats
            return await _context.Players.Include(s => s.GameStats).Where(u => u.UserID == id).ToListAsync();
        }

        public async Task<Player> GetPlayerAsyncNoTracking(int id)
        {
            // get the player with no tracking
            return await _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.PlayerID == id);
        }

        public async Task<Player> GetPlayerAsync(int id)
        {
            // get the player
            return await _context.Players.FirstOrDefaultAsync(p => p.PlayerID == id);
        }

        public bool Add(Player player)
        {
            // add the player
            _context.Players.Add(player);

            // save the changes
            return Save();
        }

        public bool Update(Player player)
        {
            // update the player
            _context.Players.Update(player);

            // save the changes
            return Save();
        }

        public bool Delete(Player player)
        {            
            // remove the player if not null
            if (player != null) 
            { 
                _context.Remove(player);
            }

            // save the changes
            return Save();
        }

        public bool Save()
        {
            // save the changes
            var saved = _context.SaveChanges();

            // return true if saved is greater than 0
            return saved > 0 ? true : false;
        }

        
    } // end repository
} // end namespace
