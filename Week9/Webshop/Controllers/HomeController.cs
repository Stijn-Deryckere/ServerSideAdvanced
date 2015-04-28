using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.BusinessLayer.Services;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private ILanguageService LanguageServ = null;
        private IApplicationUserService ApplicationUserServ = null;

        public HomeController(ILanguageService languageServ, IApplicationUserService applicationUserServ)
        {
            this.LanguageServ = languageServ;
            this.ApplicationUserServ = applicationUserServ;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult SelectLanguage()
        {
            if(HttpContext.Request.Cookies["language"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["language"];
                String id = cookie.Value;
                ViewBag.Selected = id;
            }
            List<AvailableCulture> availableCultures = this.LanguageServ.AllAvailableCultures().ToList<AvailableCulture>();
            return PartialView(availableCultures);
        }

        [HttpPost]
        public ActionResult SelectLanguage(String language)
        {
            if(HttpContext.Request.Cookies["language"] == null)
            {
                HttpCookie cookie = new HttpCookie("language");
                cookie.Value = language;
                cookie.Expires = DateTime.Now.AddDays(5);
                Response.SetCookie(cookie);
            }

            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies["language"];
                cookie.Value = language;
                cookie.Expires = DateTime.Now.AddDays(5);
                Response.SetCookie(cookie);
            }

            return RedirectToAction("Index", "Catalog");
        }
    }
}