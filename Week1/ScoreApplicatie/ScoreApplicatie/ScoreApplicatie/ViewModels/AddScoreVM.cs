using NMCT.Scores.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreApplicatie.ViewModels
{
    public class AddScoreVM
    {
        public SelectList Teams { get; set; }
        public int SelectedTeamA { get; set; }
        [Required]
        public int ScoreA { get; set; }
        public int SelectedTeamB { get; set; }
        [Required]
        public int ScoreB { get; set; }
        public int CompetitionId { get; set; }
    }
}