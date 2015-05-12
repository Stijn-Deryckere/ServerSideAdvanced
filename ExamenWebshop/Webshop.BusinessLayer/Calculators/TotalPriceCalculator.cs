using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Calculators
{
    public class TotalPriceCalculator
    {
        public List<BasketItem> BasketItems { get; set; }

        private double _totalPrice;
        public double TotalPrice
        {
            get 
            {
                CalculateTotalPrice();
                return _totalPrice; 
            }
            set { _totalPrice = value; }
        }

        public TotalPriceCalculator(List<BasketItem> basketItems)
        {
            this.BasketItems = basketItems;
        }

        private void CalculateTotalPrice()
        {
            double tempTotalPrice = 0.0;
            foreach(BasketItem basketItem in BasketItems)
            {
                tempTotalPrice += (basketItem.Amount * basketItem.RentPrice);
            }

            TotalPrice = tempTotalPrice;
        }
    }
}
