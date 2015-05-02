using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IOrderRepository
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Order> GetByApplicationUser(Webshop.Models.ApplicationUser user);
        Webshop.Models.Order Insert(Webshop.Models.Order entity);
    }
}
