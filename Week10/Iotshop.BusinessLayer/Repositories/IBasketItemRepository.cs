using System;
namespace Iotshop.BusinessLayer.Repositories
{
    public interface IBasketItemRepository
    {
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> All();
        void Delete(object id);
        Iotshop.Models.BasketItem GetByID(object id);
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> GetByUser(Iotshop.Models.ApplicationUser user);
        System.Collections.Generic.IEnumerable<Iotshop.Models.BasketItem> GetByVisitorGUID(string visitorGUID);
        Iotshop.Models.BasketItem Insert(Iotshop.Models.BasketItem entity);
        void Update(Iotshop.Models.BasketItem entityToUpdate);
        void UpdateUser(Iotshop.Models.BasketItem basketItem);
    }
}
