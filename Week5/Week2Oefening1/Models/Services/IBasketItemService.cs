using System;
namespace Week2Oefening1.Models.Services
{
    public interface IBasketItemService
    {
        Week2Oefening1.Models.BasketItem AddBasketItem(Week2Oefening1.Models.BasketItem basketItem);
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllBasketItems();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllBasketItemsOfUser(string id);
        Week2Oefening1.Models.BasketItemCountPrice GetBasketItemCountPrice(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems);
    }
}
