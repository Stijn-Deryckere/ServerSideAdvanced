using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Oefening1.Models
{
    public class ContactForm
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Firstname { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String FormType { get; set; }
        [Required]
        public String Description { get; set; }
        public Order NewOrder { get; set; }
    }
}
