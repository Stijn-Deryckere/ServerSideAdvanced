using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iotshop.Models
{
    public class Form
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        [DisplayName("Order")]
        public Order NewOrder { get; set; }
        [DisplayName("Topic")]
        public FormTopic NewFormTopic { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
