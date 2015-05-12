using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IBasketItemService
    {
        Webshop.Models.Models.BasketItem AddBasketItem(Webshop.Models.Models.BasketItem basketItem);
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.BasketItem> BasketItemsByUser(Webshop.Models.Models.ApplicationUser user);
        int CountItemsInBasket(Webshop.Models.Models.ApplicationUser user);
        Webshop.Models.Models.BasketItem CreateBasketItem(Webshop.Models.Models.Device device, Webshop.Models.Models.ApplicationUser user, int amount);
        void DeleteBasketItems(System.Collections.Generic.List<Webshop.Models.Models.BasketItem> basketItems);
        void IncrementItemsCount(Webshop.Models.Models.ApplicationUser user);
        void RefreshItemsCount(Webshop.Models.Models.ApplicationUser user);
    }
}
