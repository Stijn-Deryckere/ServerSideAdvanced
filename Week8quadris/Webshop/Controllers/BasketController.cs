﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Controllers
{
    public class BasketController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}