using Language.BusinessLayer.Services;
using Language.Models.Models;
using Language.Models.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Language.Web.Controllers
{
    public class LanguageController : Controller
    {
        private ICultureService CultureServ = null;

        public LanguageController(ICultureService cultureServ)
        {
            this.CultureServ = cultureServ;
        }

        [HttpGet]
        public ActionResult CultureChoice()
        {
            List<AvailableCulture> cultures = this.CultureServ.AllCultures().ToList<AvailableCulture>();
            AvailableCulturePM culturePM;

            if (HttpContext.Request.Cookies["language"] != null)
            {
                String value = HttpContext.Request.Cookies["language"].Value;
                AvailableCulture culture = this.CultureServ.CultureById(Convert.ToInt32(value));
                culturePM = new AvailableCulturePM()
                {
                    NewAvailableCulture = new AvailableCulture(),
                    SelectAvailableCultures = new SelectList(cultures, "ID", "Name", culture.ID)
                };
            }

            else
            {
                culturePM = new AvailableCulturePM()
                {
                    NewAvailableCulture = new AvailableCulture(),
                    SelectAvailableCultures = new SelectList(cultures, "ID", "Name")
                };
            }

            return PartialView("LanguagePartial", culturePM);
        }

        [HttpPost]
        public ActionResult CultureChoice(AvailableCulturePM culturePM)
        {
            if(HttpContext.Request.Cookies["language"] == null)
            {
                HttpCookie cookie = new HttpCookie("language");
                cookie.Expires = DateTime.Now.AddDays(5);
                cookie.Value = "" + culturePM.NewAvailableCulture.ID;
                Response.SetCookie(cookie);
            }

            else
            {
                HttpCookie cookie = HttpContext.Request.Cookies["language"];
                cookie.Value = "" + culturePM.NewAvailableCulture.ID;
                cookie.Expires = DateTime.Now.AddDays(5);
                Response.SetCookie(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}