using Language.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Language.Models.PresentationModels
{
    public class AvailableCulturePM
    {
        [Display(Name= "ChooseLang", ResourceType= typeof(Language.Models.Properties.AvailableCulturesPM.AvailableCulturesPM))]
        public AvailableCulture NewAvailableCulture { get; set; }
        public SelectList SelectAvailableCultures { get; set; }
    }
}
