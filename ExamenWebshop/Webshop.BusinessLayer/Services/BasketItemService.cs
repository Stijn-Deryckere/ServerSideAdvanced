using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Cache;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Services
{
    public class BasketItemService : Webshop.BusinessLayer.Services.IBasketItemService
    {
        private IBasketItemRepository BasketItemRepo = null;

        public BasketItemService(IBasketItemRepository basketItemRepo)
        {
            this.BasketItemRepo = basketItemRepo;
        }

        public IEnumerable<BasketItem> BasketItemsByUser(ApplicationUser user)
        {
            return this.BasketItemRepo.GetByUser(user);
        }

        public BasketItem AddBasketItem(BasketItem basketItem)
        {
            return this.BasketItemRepo.Insert(basketItem);
        }

        public BasketItem CreateBasketItem(Device device, ApplicationUser user, int amount)
        {
            return new BasketItem()
            {
                NewDevice = device,
                NewUser = user,
                RentPrice = device.RentPrice,
                Timestamp = DateTime.Now,
                Amount = amount,
                IsActive = true
            };
        }

        public void DeleteBasketItems(List<BasketItem> basketItems)
        {
            foreach(BasketItem basketItem in basketItems)
            {
                this.BasketItemRepo.Delete(basketItem);
            }
        }

        public int CountItemsInBasket(ApplicationUser user)
        {
            String userKey = user.UserName + ";Count";
            String itemsCount = WebshopCache.cache.StringGet(userKey);
            if (itemsCount != null)
            {
                int intItemsCount = 0;
                Boolean test = Int32.TryParse(itemsCount, out intItemsCount);

                return Convert.ToInt32(intItemsCount);
            }
                

            RefreshItemsCount(user);

            return CountItemsInBasket(user);
        }

        public void RefreshItemsCount(ApplicationUser user)
        {
            String userKey = user.UserName + ";Count";
            int itemsCount = this.BasketItemRepo.GetByUser(user).Count();
            WebshopCache.cache.Set(userKey, itemsCount);
        }

        public void IncrementItemsCount(ApplicationUser user)
        {
            String userKey = user.UserName + ";Count";
            WebshopCache.cache.StringIncrement(userKey, 1);
        }
    }
}
