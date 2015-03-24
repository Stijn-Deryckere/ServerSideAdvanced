using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    [Serializable]
    public class BasketItem : IItemProduct
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Device RentDevice { get; set; }
        public int Amount { get; set; }
        public ApplicationUser RentUser { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}