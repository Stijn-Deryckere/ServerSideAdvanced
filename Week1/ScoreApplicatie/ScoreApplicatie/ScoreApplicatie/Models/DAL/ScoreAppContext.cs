using NMCT.Scores.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScoreApplicatie.Models.DAL
{
    public class ScoreAppContext : DbContext
    {
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}