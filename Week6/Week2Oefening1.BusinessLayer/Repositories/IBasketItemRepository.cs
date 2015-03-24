using System;
namespace Week2Oefening1.BusinessLayer.Repositories
{
    public interface IBasketItemRepository
    {
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> All();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> AllOfUser(string id);
        Week2Oefening1.Models.BasketItem AllOfUserAndDevice(Week2Oefening1.Models.ApplicationUser user, Week2Oefening1.Models.Device device);
        void Delete(Week2Oefening1.Models.BasketItem entityToDelete);
        Week2Oefening1.Models.BasketItem Insert(Week2Oefening1.Models.BasketItem entity);
        void Update(Week2Oefening1.Models.BasketItem entityToUpdate);
    }
}
