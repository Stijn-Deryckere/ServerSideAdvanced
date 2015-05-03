using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iotshop.Models.PresentationModels
{
    public class ApplicationUserBasketItemPM
    {
        public ApplicationUser NewUser { get; set; }
        public List<BasketItem> NewBasketItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
