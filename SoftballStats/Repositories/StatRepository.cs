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

        public async Task<IEnumerable<Stats>> GetStatsAsync()
        {
            return await _context.Stats.ToListAsync();
        }

        public async Task<Stats> GetStatAsync(int id)
        {
            return await _context.Stats.FirstOrDefaultAsync(s => s.StatsID == id);
        }

        public bool Add(Stats stats)
        {
            _context.Stats.Add(stats);
            return Save();
        }

        public bool Update(Stats stats)
        {
            _context.Stats.Update(stats);
            return Save();
        }

        public bool Delete(Stats stats)
        {
            _context.Stats.Remove(stats);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    } // end repository
}// end namespace
