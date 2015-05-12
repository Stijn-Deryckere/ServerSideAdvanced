using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models.Models;

namespace Webshop.Models.PresentationModels
{
    public class BasketPM
    {
        public Device NewDevice { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
