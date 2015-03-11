using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.Models.DAL;

namespace Week2Oefening1.Models.Services
{
    public class OrderService : Week2Oefening1.Models.Services.IOrderService
    {
        private IOrderRepository repoOrder = null;

        public OrderService(IOrderRepository repoOrder)
        {
            this.repoOrder = repoOrder;
        }

        /*
         * Orders
         */
        public IEnumerable<Order> AllOrders()
        {
            return repoOrder.All();
        }

        public IEnumerable<Order> AllOrdersOfUser(String id)
        {
            return repoOrder.AllOfUser(id);
        }

        public Order AddOrder(Order order)
        {
            return repoOrder.Insert(order);
        }

        public Order MakeOrder(IEnumerable<BasketItem> basketItems)
        {
            Order order = new Order()
            {
                IsPaid = false,
                Timestamp = DateTime.Now,
                User = basketItems.First<BasketItem>().RentUser,
                TotalPrice = 0,
                OrderLines = new List<OrderLine>()
            };

            ItemCountPrice icp = new ItemCountPrice();
            foreach(BasketItem basketItem in basketItems)
            {
                OrderLine orderLine = new OrderLine()
                {
                    Amount = basketItem.Amount,
                    RentDevice = basketItem.RentDevice,
                    RentingPrice = basketItem.RentDevice.RentingPrice
                };

                order.OrderLines.Add(orderLine);

                icp.DeviceAmounts.Add(orderLine);
            }

            order.TotalPrice = icp.TotalPrice;

            return order;
        }
    }
}