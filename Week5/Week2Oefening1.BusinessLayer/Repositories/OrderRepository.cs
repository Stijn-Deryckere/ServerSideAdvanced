using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Week2Oefening1.BusinessLayer.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Week2Oefening1.Models.DAL
{
    public class OrderRepository : GenericRepository<Order>, Week2Oefening1.Models.DAL.IOrderRepository
    {
        public OrderRepository()
        { }

        public OrderRepository(ApplicationDbContext context) : base(context)
        { }

        public override IEnumerable<Order> All()
        {
            return this.context.Orders.Include(o => o.OrderLines).Include(u => u.User);
        }

        public IEnumerable<Order> AllOfUser(String id)
        {
            var query = from o in this.context.Orders.Include(l => l.OrderLines).Include(u => u.User) where o.User.Id == id select o;
            return query;
        }

        public override Order Insert(Order entity)
        {
            this.context.Entry<ApplicationUser>(entity.User).State = EntityState.Unchanged;

            foreach (OrderLine line in entity.OrderLines)
            {
                this.context.Entry<Device>(line.RentDevice).State = EntityState.Unchanged;
            }

            this.context.Orders.Add(entity);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return entity;
        }
    }
}