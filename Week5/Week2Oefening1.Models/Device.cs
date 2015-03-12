using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class Device
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double PurchasePrice { get; set; }
        public double RentingPrice { get; set; }
        public int Stock { get; set; }
        public String Image { get; set; }
        public List<Framework> DeviceFramework { get; set; }
        public List<OS> DeviceOS { get; set; }
    }
}