using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, Webshop.BusinessLayer.Repositories.IOrderRepository
    {
        public OrderRepository()
        { }

        public OrderRepository(ApplicationDbContext context)
            : base(context)
        { }

        public override Order Insert(Order entity)
        {
            this.context.Entry<ApplicationUser>(entity.NewUser).State = System.Data.Entity.EntityState.Unchanged;
            foreach (OrderLine orderLine in entity.NewOrderLines)
            {
                this.context.Entry<OrderLine>(orderLine).State = EntityState.Added;
                this.context.Entry<Device>(orderLine.NewDevice).State = System.Data.Entity.EntityState.Unchanged;

                foreach (OS os in orderLine.NewDevice.DeviceOSs)
                {
                    this.context.Entry<OS>(os).State = System.Data.Entity.EntityState.Unchanged;
                }

                foreach (Framework framework in orderLine.NewDevice.DeviceFrameworks)
                {
                    this.context.Entry<Framework>(framework).State = System.Data.Entity.EntityState.Unchanged;
                }
            }

            this.context.Orders.Add(entity);
            this.SaveChanges();

            return entity;
        }
    }
}
