using System;
namespace Week2Oefening1.BusinessLayer.Services
{
    public interface IBasketItemService
    {
        Week2Oefening1.Models.BasketItem AddBasketItem(Week2Oefening1.Models.BasketItem basketItem);
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllBasketItems();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllBasketItemsOfUser(string id);
        Week2Oefening1.Models.BasketItem AllBasketItemsOfUserAndDevice(Week2Oefening1.Models.ApplicationUser user, Week2Oefening1.Models.Device device);
        void DeleteBasketItems(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems);
        Week2Oefening1.Models.ItemCountPrice GetBasketItemCountPrice(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems);
        void UpdateBasketItem(Week2Oefening1.Models.BasketItem basketItem);
    }
}
