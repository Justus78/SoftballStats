﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SoftballStats.Models;

namespace SoftballStats.Data
{
    public class StatContext : IdentityDbContext<User>
    {

        // constructor
        public StatContext(DbContextOptions<StatContext> options) : base(options)
        {

        }

        // dbsets to set the tables in the database
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GameStats> GameStats { get; set; }

        // on model creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    } // end Context Class
} // end namespace
