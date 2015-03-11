using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class OrderLine : IItemProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Device RentDevice { get; set; }
        public double RentingPrice { get; set; }
    }
}