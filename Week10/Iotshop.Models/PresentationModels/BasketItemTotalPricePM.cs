using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iotshop.Models.PresentationModels
{
    public class BasketItemTotalPricePM
    {
        public List<BasketItem> BasketItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
