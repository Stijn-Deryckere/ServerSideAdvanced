﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCT.Scores.Models {
    public class Score {

        public int Id { get; set; }
        public Team TeamA { get; set; }
        public int ScoreA { get; set; }
        public Team TeamB { get; set; }
        public int ScoreB { get; set; }
        public int CompetitionId { get; set; }
    }
}