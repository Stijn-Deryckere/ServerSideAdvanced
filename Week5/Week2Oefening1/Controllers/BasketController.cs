using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.Models;
using Week2Oefening1.Models.Services;

namespace Week2Oefening1.Controllers
{
    public class BasketController : Controller
    {
        private IDeviceService devService = null;
        private IBasketItemService basketItemService = null;

        public BasketController(IDeviceService devService, IBasketItemService basketItemService)
        {
            this.devService = devService;
            this.basketItemService = basketItemService;
        }

        public ActionResult Index()
        {
            //Get user.
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            store.Context.Dispose();

            List<BasketItem> basketItems = basketItemService.AllBasketItemsOfUser(user.Id).ToList<BasketItem>();
            BasketItemCountPrice basketItemCountPrice = basketItemService.GetBasketItemCountPrice(basketItems);
            return View(basketItemCountPrice);
        }
    }
}