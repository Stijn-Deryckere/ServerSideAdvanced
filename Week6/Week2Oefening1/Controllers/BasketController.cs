using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week2Oefening1.BusinessLayer.Cache;
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
        private IWebshopCache webshopCache = null;

        public BasketController(IDeviceService devService, IBasketItemService basketItemService, IOrderService orderService, IUserService userService, IWebshopCache webshopCache)
        {
            this.devService = devService;
            this.basketItemService = basketItemService;
            this.orderService = orderService;
            this.userService = userService;
            this.webshopCache = webshopCache;
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

        [HttpPost]
        public ActionResult Index(int id, int amount)
        {
            ApplicationUser user = userService.UserByName(User.Identity.Name);

            List<BasketItem> basketItems = basketItemService.AllBasketItemsOfUser(user.Id).ToList<BasketItem>();
            foreach(BasketItem basketItem in basketItems)
            {
                if(basketItem.RentDevice.Id.Equals(id))
                {
                    basketItem.Amount = amount;
                    basketItemService.UpdateBasketItem(basketItem);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public int CountBasketItems()
        {
            //Get user. 
            ApplicationUser user = userService.UserByName(User.Identity.Name);

            int count = webshopCache.GetNumberOfBasketItemsOfUser(user);
            return count;
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            //Get user. 
            ApplicationUser user = userService.UserByName(User.Identity.Name);

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
            orderService.SendOrderMail(finalOrder);
            basketItemService.DeleteBasketItems(basketItems1);

            return RedirectToAction("Index");
        }
    }
}