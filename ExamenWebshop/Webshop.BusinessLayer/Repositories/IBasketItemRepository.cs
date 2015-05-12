using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IBasketItemRepository
    {
        void Delete(Webshop.Models.Models.BasketItem entityToDelete);
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.BasketItem> GetByUser(Webshop.Models.Models.ApplicationUser user);
        Webshop.Models.Models.BasketItem Insert(Webshop.Models.Models.BasketItem entity);
    }
}
