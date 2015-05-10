using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, Webshop.BusinessLayer.Repositories.IDeviceRepository 
    {
        public DeviceRepository()
        {
        
        }

        public DeviceRepository(ApplicationDbContext context)
            :base(context)
        {

        }

        public override IEnumerable<Device> All()
        {
            return this.context.Devices.Include(o => o.DeviceOSs).Include(f => f.DeviceFrameworks);
        }

        public override Device GetByID(object id)
        {
            return this.context.Devices.Include(o => o.DeviceOSs).Include(f => f.DeviceFrameworks).Where(d => d.ID == (int)id).SingleOrDefault<Device>();
        }

        public override Device Insert(Device entity)
        {
            foreach(OS os in entity.DeviceOSs)
            {
                this.context.Entry<OS>(os).State = EntityState.Unchanged;
            }

            foreach(Framework framework in entity.DeviceFrameworks)
            {
                this.context.Entry<Framework>(framework).State = EntityState.Unchanged;
            }

            this.context.Devices.Add(entity);
            SaveChanges();

            return entity;
        }
    }
}
