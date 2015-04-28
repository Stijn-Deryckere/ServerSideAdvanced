using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IBasketItemService
    {
        Webshop.Models.BasketItem AddBasketItem(Webshop.Models.BasketItem basketItem);
        System.Collections.Generic.IEnumerable<Webshop.Models.BasketItem> AllBasketItems();
        Webshop.Models.BasketItem BasketItemById(int id);
        System.Collections.Generic.IEnumerable<Webshop.Models.BasketItem> BasketItemsByUser(Webshop.Models.ApplicationUser user);
        void UpdateBasketItem(Webshop.Models.BasketItem basketItem);
    }
}
