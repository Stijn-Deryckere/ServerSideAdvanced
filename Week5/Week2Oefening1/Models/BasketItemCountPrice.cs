using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class BasketItemCountPrice
    {
        public Dictionary<int, Device> DeviceAmounts { get; set; }

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

        public BasketItemCountPrice()
        {
            this.DeviceAmounts = new Dictionary<int, Device>();
        }

        private void CalculateTotalPrice()
        {
            foreach(KeyValuePair<int, Device> deviceAmount in DeviceAmounts)
            {
                this._totalPrice += deviceAmount.Key * deviceAmount.Value.RentingPrice;
            }
        }
    }
}