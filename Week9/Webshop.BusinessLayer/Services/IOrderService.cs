using System;
namespace Webshop.BusinessLayer.Services
{
    pubic interface IOrderService
    {
        Webshop.Models.Order AddOrder(Webshop.Models.Order order);
        Webshop.Models.Order GenerateOrder(Webshop.Models.ApplicationUser user, System.Collections.Generic.List<Webshop.Models.BasketItem> basketItems, double totalPrice);
        Webshop.Models.Order OrderByID(int id);
        System.Collections.Generic.IEnumerable<Webshop.Models.Order> OrdersByApplicationUser(Webshop.Models.ApplicationUser user);
        Webshop.Models.Order SaveOrder(Webshop.Models.Order order);
    }
}
