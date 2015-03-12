using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public ApplicationUser User { get; set; }
        public Boolean IsPaid { get; set; }
        public Boolean IsDelivered { get; set; }
        public double TotalPrice { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Zipcode { get; set; }
    }
}