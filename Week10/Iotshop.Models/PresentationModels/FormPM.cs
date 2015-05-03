
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Iotshop.Models.PresentationModels
{
    public class FormPM
    {
        public Form NewForm { get; set; }
        public SelectList NewFormTopics { get; set; }
        [Required]
        public int NewFormTopicsID { get; set; }
        public SelectList NewOrders { get; set; }
        public int NewOrdersID { get; set; }
    }
}
