using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Week2Oefening1.BusinessLayer.Context;

namespace Week2Oefening1.Models.DAL
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository()
        { }
        public DeviceRepository(ApplicationDbContext context) : base(context)
        { }

        /*
         * Overriden wnat we hebben meer nodig dan enkel device (ook OS en Framework).
         */
        public override IEnumerable<Device> All()
        {
            return this.context.Devices.AsNoTracking<Device>().Include(f => f.DeviceFramework).Include(o => o.DeviceOS);
        }

        public override Device GetByID(object id)
        {
            int tempId = (int)id;
            var query = from d in this.context.Devices.AsNoTracking<Device>().Include(f => f.DeviceFramework).Include(o => o.DeviceOS) where d.Id == tempId select d;
            return query.SingleOrDefault<Device>();
        }

        /*
         * Hier overriden want hier hebben we een insert met params (want OS en Framework includen).
         */
        public override Device Insert(Device entity)
        {
            foreach (OS os in entity.DeviceOS)
                this.context.Entry<OS>(os).State = EntityState.Unchanged;

            foreach (Framework fw in entity.DeviceFramework)
                this.context.Entry<Framework>(fw).State = EntityState.Unchanged;

            this.context.Devices.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}