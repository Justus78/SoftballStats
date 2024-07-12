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
            return await _context.GameStats.Where(p => p.PlayerID == id).ToListAsync();
        }

        public async Task<GameStats> GetStatAsync(int id)
        {
            return await _context.GameStats.FirstOrDefaultAsync(s => s.StatsID == id);
        }

        public bool Add(GameStats stats)
        {
            _context.GameStats.Add(stats);
            return Save();
        }

        public bool Update(GameStats stats)
        {
            _context.GameStats.Update(stats);
            return Save();
        }

        public bool Delete(GameStats stats)
        {
            _context.GameStats.Remove(stats);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    } // end repository
}// end namespace
