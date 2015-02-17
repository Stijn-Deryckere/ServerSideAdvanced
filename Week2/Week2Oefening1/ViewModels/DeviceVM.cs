using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.Models;

namespace Week2Oefening1.ViewModels
{
    public class DeviceVM
    {
        public Device NewDevice { get; set; }
        public int[] NewOperatingSystems { get; set; }
        public SelectList OperatingSystems { get; set; }
        public int[] NewFrameworks { get; set; }
        public SelectList Frameworks { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}