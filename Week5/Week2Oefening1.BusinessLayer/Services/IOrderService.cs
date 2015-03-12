using System;
namespace Week2Oefening1.BusinessLayer.Services
{
    public interface IOrderService
    {
        global::Week2Oefening1.Models.Order AddOrder(global::Week2Oefening1.Models.Order order);
        global::Week2Oefening1.Models.Order AddOrderToDatabase(global::Week2Oefening1.Models.Order order);
        global::System.Collections.Generic.IEnumerable<global::Week2Oefening1.Models.Order> AllOrders();
        global::System.Collections.Generic.IEnumerable<global::Week2Oefening1.Models.Order> AllOrdersOfUser(string id);
        global::Week2Oefening1.Models.Order MakeOrder(global::System.Collections.Generic.IEnumerable<global::Week2Oefening1.Models.BasketItem> basketItems);
        global::Week2Oefening1.Models.Order MakeOrder(global::System.Collections.Generic.IEnumerable<global::Week2Oefening1.Models.BasketItem> basketItems, global::Week2Oefening1.Models.Order previousOrder);
    }
}
