using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NMCT.Scores.Models;

namespace ScoreApplicatie.Models.DAL
{
    public class TeamRepository
    {
        public List<Team> GetTeams(int competitionID)
        {
            using (ScoreAppContext context = new ScoreAppContext())
            {
                var query = (from t in context.Teams where t.Competition.Id == competitionID orderby t.Name select t);
                return query.ToList<Team>();
            }
        }

        public Team GetTeam(int teamID)
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                var query = (from t in context.Teams where t.Id == teamID select t);
                return query.SingleOrDefault<Team>();
            }
        }
    }
}