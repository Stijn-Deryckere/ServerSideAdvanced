using System;
namespace Iotshop.BusinessLayer.Repositories
{
    public interface IOrderRepository
    {
        System.Collections.Generic.IEnumerable<Iotshop.Models.Order> GetByApplicationUser(Iotshop.Models.ApplicationUser user);
        Iotshop.Models.Order GetByID(object id);
        Iotshop.Models.Order Insert(Iotshop.Models.Order entity);
    }
}
