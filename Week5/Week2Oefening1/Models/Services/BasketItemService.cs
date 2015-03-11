using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.Models.DAL;

namespace Week2Oefening1.Models.Services
{
    public class BasketItemService : Week2Oefening1.Models.Services.IBasketItemService 
    {
        private IBasketItemRepository repoBasketItem = null;

        public BasketItemService(IBasketItemRepository repoBasketItem)
        {
            this.repoBasketItem = repoBasketItem;
        }

        public BasketItemService()
        { }

        /*
         * Basketitems
         */
        public IEnumerable<BasketItem> AllBasketItems()
        {
            return repoBasketItem.All();
        }

        public BasketItem AddBasketItem(BasketItem basketItem)
        {
            return repoBasketItem.Insert(basketItem);
        }

        public IEnumerable<BasketItem> AllBasketItemsOfUser(String id)
        {
            return repoBasketItem.AllOfUser(id);
        }

        /*
         * BasketItemCountPrice
         */
        public ItemCountPrice GetBasketItemCountPrice(IEnumerable<BasketItem> basketItems)
        {
            ItemCountPrice basketItemCountPrice = new ItemCountPrice();
            foreach(BasketItem basketItem in basketItems)
            {
                basketItemCountPrice.DeviceAmounts.Add(basketItem);
            }

            return basketItemCountPrice;
        }
    }
}