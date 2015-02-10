using NMCT.Scores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreApplicatie.ViewModels
{
    public class TeamsVM
    {
        public SelectList Competitions { get; set; }
        public List<Team> Teams { get; set; }
        public int SelectedCompetition { get; set; }
    }
}