using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.BusinessLayer.Context;
using Week2Oefening1.BusinessLayer.Services;
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
        private IUserService userService = null;

        public BasketController(IDeviceService devService, IBasketItemService basketItemService, IOrderService orderService, IUserService userService)
        {
            this.devService = devService;
            this.basketItemService = basketItemService;
            this.orderService = orderService;
            this.userService = userService;
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
            order.User = userService.UserByName(User.Identity.Name);

            List<BasketItem> basketItems1 = basketItemService.AllBasketItemsOfUser(order.User.Id).ToList<BasketItem>();
            Order finalOrder = orderService.MakeOrder(basketItems1, order);

            orderService.AddOrder(finalOrder);
            List<BasketItem> basketItems2 = basketItemService.AllBasketItemsOfUser(order.User.Id).ToList<BasketItem>();
            basketItemService.DeleteBasketItems(basketItems2);

            return RedirectToAction("Index");
        }
    }
}