using SoftballStats.Models;
using SoftballStats.Interfaces;
using SoftballStats.Data;
using Microsoft.EntityFrameworkCore;

namespace SoftballStats.Repositories
{
    public class TeamRepository : ITeam
    {

        // member variable to hold the context
        private readonly StatContext _context;

        // add the contect in the constructor
        public TeamRepository(StatContext context)
        {
            _context = context;
        } // end constructor

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamAsync(int id)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.TeamID == id);
        }

        public bool Add(Team team)
        {
            _context.Teams.Add(team);
            return Save();
        }

        public bool Update(Team team)
        {
            _context.Teams.Update(team);
            return Save();
        }

        public bool Delete(Team team)
        {
            _context.Remove(team);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    } // end repository
} // end namespace
