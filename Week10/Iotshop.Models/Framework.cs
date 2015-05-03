using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iotshop.Models
{
    public class Framework
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}
