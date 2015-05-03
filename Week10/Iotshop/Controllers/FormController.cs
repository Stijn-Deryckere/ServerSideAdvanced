using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Iotshop.BusinessLayer.Services;
using Iotshop.Models;
using Iotshop.Models.PresentationModels;

namespace Iotshop.Controllers
{
    public class FormController : Controller
    {
        private IFormService FormServ = null;
        private IOrderService OrderServ = null;
        private IApplicationUserService ApplicationUserServ = null;

        public FormController(IFormService formServ, IOrderService orderServ, IApplicationUserService applicationUserServ)
        {
            this.FormServ = formServ;
            this.OrderServ = orderServ;
            this.ApplicationUserServ = applicationUserServ;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Order> orders = new List<Order>();
            if(User.Identity.IsAuthenticated)
            {
                ApplicationUser user = this.ApplicationUserServ.ApplicationUserByName(User.Identity.Name);
                orders = this.OrderServ.OrdersByApplicationUser(user).ToList<Order>();
            }

            List<FormTopic> formTopics = this.FormServ.AllFormTopics().ToList<FormTopic>();
            FormPM formPM = new FormPM()
            {
                NewForm = new Form(),
                NewFormTopics = new SelectList(formTopics, "ID", "Name"),
                NewOrders = new SelectList(orders, "ID", "Timestamp")
            };
            return View(formPM);
        }

        [HttpPost]
        public ActionResult Index(FormPM formPM)
        {
            if(ModelState.IsValid)
            {
                Order order = OrderServ.OrderByID(formPM.NewOrdersID);
                FormTopic formTopic = FormServ.FormTopicByID(formPM.NewFormTopicsID);
                if(order != null)
                    formPM.NewForm.NewOrder = order;
                formPM.NewForm.NewFormTopic = formTopic;
                this.FormServ.AddForm(formPM.NewForm);
            }

            return RedirectToAction("Index", "Catalog");
        }
    }
}