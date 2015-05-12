using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models.Models
{
    [Serializable]
    public class OS
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }

        public List<Device> Devices { get; set; }
    }
}
