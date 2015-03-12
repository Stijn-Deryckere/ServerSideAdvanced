using System;
namespace Week2Oefening1.Models.DAL
{
    public interface IBasketItemRepository
    {
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> All();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllOfUser(string id);
        void Delete(Week2Oefening1.Models.BasketItem entityToDelete);
        Week2Oefening1.Models.BasketItem Insert(Week2Oefening1.Models.BasketItem entity);
    }
}
