using System;
namespace Week2Oefening1.BusinessLayer.Services
{
    public interface IOrderService
    {
        Week2Oefening1.Models.Order AddOrder(Week2Oefening1.Models.Order order);
        Week2Oefening1.Models.Order AddOrderToDatabase(Week2Oefening1.Models.Order order);
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> AllOrders();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> AllOrdersOfUser(string id);
        Week2Oefening1.Models.Order MakeOrder(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems);
        Week2Oefening1.Models.Order MakeOrder(System.Collections.Generic.IEnumerable<Week2Oefening1.Models.BasketItem> basketItems, Week2Oefening1.Models.Order previousOrder);
        void SendOrderMail(Week2Oefening1.Models.Order order);
    }
}
