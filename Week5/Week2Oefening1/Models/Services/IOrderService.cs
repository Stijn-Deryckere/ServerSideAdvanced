using System;
namespace Week2Oefening1.Models.Services
{
    public interface IOrderService
    {
        Week2Oefening1.Models.Order AddOrder(Week2Oefening1.Models.Order order);
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> AllOrders();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> AllOrdersOfUser(string id);
        Week2Oefening1.Models.Order MakeOrder(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems);
    }
}
