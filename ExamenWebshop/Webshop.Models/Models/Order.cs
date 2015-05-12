using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models.Models
{
    [Serializable]
    public class Order
    {
        public int ID { get; set; }

        [Required]
        public List<OrderLine> NewOrderLines { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public ApplicationUser NewUser { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
