using Iotshop.BusinessLayer.Services;
using Iotshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iotshop.Controllers
{
    public class HomeController : Controller
    {
        private ILanguageService LanguageServ = null;

        public HomeController(ILanguageService languageServ)
        {
            this.LanguageServ = languageServ;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult SelectLanguage()
        {
            if (HttpContext.Request.Cookies["language"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["language"];
                String id = cookie.Value;
                ViewBag.Selected = id;
            }
            List<AvailableCulture> availableCultures = this.LanguageServ.AllAvailableCultures().ToList<AvailableCulture>();
            return PartialView(availableCultures);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}