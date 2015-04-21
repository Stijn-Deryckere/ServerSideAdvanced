using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Device
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public double RentingPrice { get; set; }
        [Required]
        public int Stock { get; set; }
        public String Image { get; set; }
        public String Description { get; set; }
        public List<OS> DeviceOS { get; set; }
        public List<Framework> DeviceFramework { get; set; }
    }
}
