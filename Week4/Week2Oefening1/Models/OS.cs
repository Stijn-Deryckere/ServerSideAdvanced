﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models
{
    public class OS
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}