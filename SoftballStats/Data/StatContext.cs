using Microsoft.EntityFrameworkCore;
using SoftballStats.Models;

namespace SoftballStats.Data
{
    public class StatContext : DbContext
    {
        public StatContext(DbContextOptions<StatContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GameStats> Stats { get; set; }
    } // end Context Class
} // end namespace
