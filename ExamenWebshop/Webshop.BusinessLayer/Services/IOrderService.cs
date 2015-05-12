using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IOrderService
    {
        Webshop.Models.Models.Order AddOrder(Webshop.Models.Models.Order order);
        Webshop.Models.Models.Order MakeOrder(System.Collections.Generic.List<Webshop.Models.Models.BasketItem> basketItems, Webshop.Models.Models.ApplicationUser user);
        Webshop.Models.Models.Order SaveOrder(Webshop.Models.Models.Order order);
    }
}
