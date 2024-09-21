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

        public async Task<IEnumerable<Team>> GetTeamsAsync(string id)
        {
            // get the teams for the user
            return await _context.Teams.Where(u => u.UserID == id).ToListAsync();
        }

        public async Task<Team> GetTeamAsync(int id)
        {
            // get the team
            return await _context.Teams.FirstOrDefaultAsync(t => t.TeamID == id);
        }

        public async Task<Team> GetTeamAsyncNoTracking(int id)
        {
            // get the team with no tracking
            return await _context.Teams.AsNoTracking().FirstOrDefaultAsync(t => t.TeamID == id);
        }

        public bool Add(Team team)
        {
            // add the team
            _context.Teams.Add(team);

            // save the changes
            return Save();
        }

        public bool Update(Team team)
        {
            // update the team
            _context.Teams.Update(team);

            // save the changes
            return Save();
        }

        public bool Delete(Team team)
        {
            // remove the team
            _context.Remove(team);

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
