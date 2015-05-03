using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iotshop.Models
{
    public class OrderLine
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public double RentingPrice { get; set; }
        [Required]
        public Device NewDevice { get; set; }
    }
}
