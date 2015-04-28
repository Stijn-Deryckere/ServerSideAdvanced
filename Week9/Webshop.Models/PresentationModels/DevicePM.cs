using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Models.PresentationModels
{
    public class DevicePM
    {
        public Device NewDevice { get; set; }
        [Required]
        public HttpPostedFileBase ImageFile { get; set; }
        public int[] NewOperatingSystems { get; set; }
        public SelectList OSs { get; set; }
        public int[] NewFrameworks { get; set; }
        public SelectList Frameworks { get; set; }
    }
}
