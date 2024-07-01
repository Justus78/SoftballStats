using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.Data;

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

        public Task<IEnumerable<Stats>> GetStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stats> GetStatAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Stats stats)
        {
            throw new NotImplementedException();
        }

        public bool Update(Stats stats)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    } // end repository
}// end namespace
