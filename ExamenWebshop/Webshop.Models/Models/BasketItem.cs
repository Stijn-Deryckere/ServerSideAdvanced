﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models.Models
{
    [Serializable]
    public class BasketItem
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public Device NewDevice { get; set; }

        [Required]
        public ApplicationUser NewUser { get; set; }

        [Required]
        public double RentPrice { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public Boolean IsActive { get; set; }
    }
}
