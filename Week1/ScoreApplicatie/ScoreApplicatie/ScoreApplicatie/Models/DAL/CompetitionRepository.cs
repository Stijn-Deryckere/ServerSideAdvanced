using NMCT.Scores.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreApplicatie.Models.DAL
{
    public class CompetitionRepository
    {
        public List<Competition> GetCompetititions()
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                var query = (from c in context.Competitions.Include(c => c.Country) orderby c.Name select c);
                return query.ToList<Competition>();
            }
        }

        public Competition GetCompetition(int id)
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                var query = (from c in context.Competitions.Include(c => c.Country)
                                 .Include(s => s.Scores.Select(t => t.TeamA))
                                 .Include(s => s.Scores.Select(t => t.TeamB))
                             where c.Id == id
                             select c);
                return query.SingleOrDefault<Competition>();
            }
        }

        public void AddScore(Score score)
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                context.Scores.Add(score);
                context.Entry<Team>(score.TeamA).State = EntityState.Unchanged;
                context.Entry<Team>(score.TeamB).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }

        public Score GetScore(int id)
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                var query = (from s in context.Scores.Include(t => t.TeamA).Include(t => t.TeamB) where s.Id == id select s);
                return query.SingleOrDefault<Score>();
            }
        }

        public void DeleteScore(Score score)
        {
            using(ScoreAppContext context = new ScoreAppContext())
            {
                context.Entry<Team>(score.TeamA).State = EntityState.Unchanged;
                context.Entry<Team>(score.TeamB).State = EntityState.Unchanged;
                context.Scores.Attach(score);
                context.Scores.Remove(score);
                context.SaveChanges();
            }
        }
    }
}