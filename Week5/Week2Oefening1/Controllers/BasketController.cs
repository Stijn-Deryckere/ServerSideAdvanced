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
    [Authorize]
    public class BasketController : Controller
    {
        private IDeviceService devService = null;
        private IBasketItemService basketItemService = null;
        private IOrderService orderService = null;

        public BasketController(IDeviceService devService, IBasketItemService basketItemService, IOrderService orderService)
        {
            this.devService = devService;
            this.basketItemService = basketItemService;
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Get user.
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            store.Context.Dispose();

            List<BasketItem> basketItems = basketItemService.AllBasketItemsOfUser(user.Id).ToList<BasketItem>();
            ItemCountPrice basketItemCountPrice = basketItemService.GetBasketItemCountPrice(basketItems);
            return View(basketItemCountPrice);
        }

        [HttpGet]
        public int CountBasketItems()
        {
            //Get user. 
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            store.Context.Dispose();

            int count = basketItemService.AllBasketItemsOfUser(user.Id).ToList<BasketItem>().Count;
            return count;
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            //Get user. 
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            store.Context.Dispose();

            List<BasketItem> basketItems = basketItemService.AllBasketItemsOfUser(user.Id).ToList<BasketItem>();
            Order order = orderService.MakeOrder(basketItems);

            return View(order);
        }

        [HttpPost]
        public ActionResult FinalCheckOut(Order order)
        {
            //Get user. 
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            store.Context.Dispose();

            order.User = user;

            List<BasketItem> basketItems = basketItemService.AllBasketItemsOfUser(order.User.Id).ToList<BasketItem>();
            Order finalOrder = orderService.MakeOrder(basketItems);

            orderService.AddOrder(finalOrder);

            return RedirectToAction("Index");
        }
    }
}