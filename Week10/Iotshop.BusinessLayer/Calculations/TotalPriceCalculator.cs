using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iotshop.Models;

namespace Iotshop.BusinessLayer.Calculations
{
    public class TotalPriceCalculator
    {
        public List<BasketItem> BasketItems { get; set; }
        private double _totalPrice;  
        public double TotalPrice
        {
            get 
            {
                GetTotalPrice();
                return _totalPrice; 
            }
            set { _totalPrice = value; }
        }
        

        public TotalPriceCalculator(List<BasketItem> basketItems)
        {
            this.BasketItems = basketItems;
        }

        public void GetTotalPrice()
        {
            double tempTotalPrice = 0;

            foreach(BasketItem basketItem in this.BasketItems)
            {
                tempTotalPrice += basketItem.Amount * basketItem.RentingPrice;
            }

            this.TotalPrice = tempTotalPrice;
        }
    }
}
