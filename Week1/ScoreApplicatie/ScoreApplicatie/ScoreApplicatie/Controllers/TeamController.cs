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
    public class TeamController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CompetitionRepository repoComp = new CompetitionRepository();
            List<Competition> competitions = repoComp.GetCompetititions();

            TeamsVM vm = new TeamsVM()
            {
                Competitions = new SelectList(competitions, "Id", "Name")
            };

            if (vm.Teams == null)
                vm.Teams = new List<Team>();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(TeamsVM tempVM)
        {
            CompetitionRepository repoComp = new CompetitionRepository();
            List<Competition> competitions = repoComp.GetCompetititions();

            TeamRepository repoTeam = new TeamRepository();
            List<Team> teams = repoTeam.GetTeams(tempVM.SelectedCompetition);

            TeamsVM vm = new TeamsVM()
            {
                Competitions = new SelectList(competitions, "Id", "Name"),
                Teams = teams
            };

            if (vm.Teams == null)
                vm.Teams = new List<Team>();

            return View(vm);
        }
    }
}