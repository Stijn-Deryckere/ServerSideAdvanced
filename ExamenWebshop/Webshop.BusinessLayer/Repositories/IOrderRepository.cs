using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IOrderRepository
    {
        Webshop.Models.Models.Order Insert(Webshop.Models.Models.Order entity);
    }
}
