using System;
namespace Week2Oefening1.Models.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> All();
        System.Collections.Generic.IEnumerable<Week2Oefening1.Models.Order> AllOfUser(string id);
        Week2Oefening1.Models.Order Insert(Week2Oefening1.Models.Order entity);
    }
}
