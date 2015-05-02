using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IOrderService
    {
        Webshop.Models.Order AddOrder(Webshop.Models.Order order);
        Webshop.Models.Order GenerateOrder(Webshop.Models.ApplicationUser user, System.Collections.Generic.List<Webshop.Models.BasketItem> basketItems, double totalPrice);
        System.Collections.Generic.IEnumerable<Webshop.Models.Order> OrdersByApplicationUser(Webshop.Models.ApplicationUser user);
    }
}
