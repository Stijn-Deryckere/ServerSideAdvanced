using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface IOrderService
    {
        Iotshop.Models.Order AddOrder(Iotshop.Models.Order order);
        Iotshop.Models.Order GenerateOrder(Iotshop.Models.ApplicationUser user, System.Collections.Generic.List<Iotshop.Models.BasketItem> basketItems, double totalPrice);
        Iotshop.Models.Order OrderByID(int id);
        System.Collections.Generic.IEnumerable<Iotshop.Models.Order> OrdersByApplicationUser(Iotshop.Models.ApplicationUser user);
        Iotshop.Models.Order SaveOrder(Iotshop.Models.Order order);
    }
}
