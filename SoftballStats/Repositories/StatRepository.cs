using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.Data;
using Microsoft.EntityFrameworkCore;

namespace SoftballStats.Repositories
{
    public class StatRepository : IStats
    {

        // member variable to hold the context
        private readonly StatContext _context;

        // add the contect in the constructor
        public StatRepository(StatContext context)
        {
            _context = context;
        } // end constructor

        public async Task<IEnumerable<GameStats>> GetStatsAsync(int id)
        {
            // get the stats for the player
            return await _context.GameStats.Where(p => p.PlayerID == id).ToListAsync();
        }

        public async Task<GameStats> GetStatAsync(int id)
        {
            // get the stat for the player
            return await _context.GameStats.Include(p => p.Player).FirstOrDefaultAsync(s => s.StatsID == id);
        }

        public bool Add(GameStats stats)
        {
            // add the stats
            _context.GameStats.Add(stats);

            // save the changes
            return Save();
        }

        public bool Update(GameStats stats)
        {
            // update the stats
            _context.GameStats.Update(stats);

            // save the changes
            return Save();
        }

        public bool Delete(GameStats stats)
        {
            // remove the stats
            _context.GameStats.Remove(stats);

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
}// end namespace
