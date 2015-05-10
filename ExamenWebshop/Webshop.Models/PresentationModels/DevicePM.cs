using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webshop.Models.Models;
using System.Web;

namespace Webshop.Models.PresentationModels
{
    public class DevicePM
    {
        public Device NewDevice { get; set; }
        public int[] SelectedOSs { get; set; }
        public int[] SelectedFrameworks { get; set; }
        public HttpPostedFileBase NewPicture { get; set; }
    }
}
