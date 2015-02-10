using NMCT.Scores.Models;
using ScoreApplicatie.Models.DAL;
using ScoreApplicatie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreApplicatie.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Index()
        {
            CompetitionRepository repo = new CompetitionRepository();
            List<Competition> competitions = new List<Competition>();
            competitions = repo.GetCompetititions();
            return View(competitions);
        }

        public ActionResult Details(int id)
        {
            CompetitionRepository repo = new CompetitionRepository();
            Competition competition = repo.GetCompetition(id);
            return View(competition);
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            TeamRepository repo = new TeamRepository();
            List<Team> teams = repo.GetTeams(id);
            AddScoreVM adsvm = new AddScoreVM()
            {
                CompetitionId = id,
                Teams = new SelectList(teams, "Id", "Name")
            };
            return View(adsvm);
        }

        [HttpPost]
        public ActionResult New(AddScoreVM adsvm)
        {
            TeamRepository repo = new TeamRepository();
            Score score = new Score();
            score.ScoreA = adsvm.ScoreA;
            score.ScoreB = adsvm.ScoreB;
            score.TeamA = repo.GetTeam(adsvm.SelectedTeamA);
            score.TeamB = repo.GetTeam(adsvm.SelectedTeamB);
            score.CompetitionId = adsvm.CompetitionId;

            CompetitionRepository repoComp = new CompetitionRepository();
            repoComp.AddScore(score);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            CompetitionRepository repo = new CompetitionRepository();
            Score score = repo.GetScore(id);
            return View(score);
        }

        [HttpPost]
        public ActionResult Delete(Score tempScore)
        {
            CompetitionRepository repo = new CompetitionRepository();
            Score score = repo.GetScore(tempScore.Id);
            repo.DeleteScore(score);
            return RedirectToAction("Index");
        }
    }
}