using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using System.Data.Entity;
using Webshop.BusinessLayer.Context;

namespace Webshop.BusinessLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, Webshop.BusinessLayer.Repositories.IOrderRepository
    {
        public OrderRepository()
        { }

        public OrderRepository(WebshopContext context)
            :base(context)
        { }

        public override Order GetByID(object id)
        {
            return this.context.Orders.Include(u => u.NewUser).Include(o => o.NewOrderLines).Where(o => o.ID == (int)id).SingleOrDefault<Order>();
        }

        public override Order Insert(Order entity)
        {
            this.context.Entry<ApplicationUser>(entity.NewUser).State = System.Data.Entity.EntityState.Unchanged;
            foreach(OrderLine orderLine in entity.NewOrderLines)
            {
                this.context.Entry<Device>(orderLine.NewDevice).State = System.Data.Entity.EntityState.Unchanged;

                foreach(OS os in orderLine.NewDevice.DeviceOS)
                {
                    this.context.Entry<OS>(os).State = System.Data.Entity.EntityState.Unchanged;
                }
                
                foreach(Framework framework in orderLine.NewDevice.DeviceFramework)
                {
                    this.context.Entry<Framework>(framework).State = System.Data.Entity.EntityState.Unchanged;
                }
            }

            this.context.Orders.Add(entity);
            this.SaveChanges();

            return entity;
        }

        public IEnumerable<Order> GetByApplicationUser(ApplicationUser user)
        {
            return this.context.Orders.Include(o => o.NewOrderLines).Include(u => u.NewUser).Where(u => u.NewUser.Id == user.Id);
        }
    }
}
