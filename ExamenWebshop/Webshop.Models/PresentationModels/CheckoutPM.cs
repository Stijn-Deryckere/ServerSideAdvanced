using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models.Models;

namespace Webshop.Models.PresentationModels
{
    public class CheckoutPM
    {
        public List<BasketItem> BasketItems { get; set; }
        public ApplicationUser NewUser { get; set; }
    }
}
