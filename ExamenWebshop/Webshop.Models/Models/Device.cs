using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models.Models
{
    [Serializable]
    public class Device
    {
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }
        
        [Required]
        public double RentPrice { get; set; }

        [Required]
        public double PurchasePrice { get; set; }
        
        [Required]
        public int Stock { get; set; }

        public String Picture { get; set; }

        public List<OS> DeviceOSs { get; set; }

        public List<Framework> DeviceFrameworks { get; set; }

        [Required]
        public String Description { get; set; }
    }
}
