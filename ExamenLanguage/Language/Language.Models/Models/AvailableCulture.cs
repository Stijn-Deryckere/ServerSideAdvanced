﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Models.Models
{
    public class AvailableCulture
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Code { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
