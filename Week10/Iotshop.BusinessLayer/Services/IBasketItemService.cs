using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface IBasketItemService
    {
        Iotshop.Models.BasketItem AddBasketItem(Iotshop.Models.BasketItem basketItem);
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> AllBasketItems();
        Iotshop.Models.BasketItem BasketItemById(int id);
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> BasketItemsByUser(Iotshop.Models.ApplicationUser user);
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> BasketItemsByVisitorGUID(string visitorGUID);
        void DeleteBasketItem(int id);
        void UpdateBasketItem(Iotshop.Models.BasketItem basketItem);
        void UpdateBasketItemUser(Iotshop.Models.BasketItem basketItem);
    }
}
