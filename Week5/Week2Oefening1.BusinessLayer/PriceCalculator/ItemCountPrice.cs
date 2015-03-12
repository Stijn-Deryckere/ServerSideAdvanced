using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class ItemCountPrice
    {
        public List<IItemProduct> DeviceAmounts { get; set; }

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

        public ItemCountPrice()
        {
            this.DeviceAmounts = new List<IItemProduct>();
        }

        private void CalculateTotalPrice()
        {
            foreach(IItemProduct deviceAmount in DeviceAmounts)
            {
                this._totalPrice += deviceAmount.Amount * deviceAmount.RentDevice.RentingPrice;
            }
        }
    }
}