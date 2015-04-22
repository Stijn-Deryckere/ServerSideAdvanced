using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models;
using System.Data.Entity;

namespace Webshop.BusinessLayer.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, Webshop.BusinessLayer.Repositories.IDeviceRepository
    {
        public DeviceRepository()
        { }

        public DeviceRepository(WebshopContext context) : base(context)
        { }

        public override IEnumerable<Device> All()
        {
            return this.context.Devices.Include(o => o.DeviceOS).Include(f => f.DeviceFramework).ToList<Device>();
        }

        public override Device GetByID(object id)
        {
            int tempId = (int)id;
            var query = from d in this.context.Devices.Include(o => o.DeviceOS).Include(f => f.DeviceFramework) where d.ID == tempId select d;
            return query.SingleOrDefault<Device>();
        }

        public override Device Insert(Device entity)
        {
            foreach(Framework framework in entity.DeviceFramework)
                this.context.Entry<Framework>(framework).State = EntityState.Unchanged;

            foreach (OS os in entity.DeviceOS)
                this.context.Entry<OS>(os).State = EntityState.Unchanged;

            this.context.Devices.Add(entity);
            this.context.SaveChanges();

            return entity;
        }
    }
}
