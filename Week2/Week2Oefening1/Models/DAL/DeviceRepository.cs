using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Week2Oefening1.Models.DAL
{
    public class DeviceRepository
    {
        public static List<Device> Get()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from d in context.Devices select d);
                return query.ToList<Device>();
            }
        }

        public static Device Get(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from d in context.Devices.Include(f => f.DeviceFramework).Include(o => o.DeviceOS) where d.Id == id select d);
                return query.SingleOrDefault<Device>();
            }
        }

        public static void Post(Device device)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach (OS os in device.DeviceOS)
                    context.Entry<OS>(os).State = EntityState.Unchanged;

                foreach (Framework fw in device.DeviceFramework)
                    context.Entry<Framework>(fw).State = EntityState.Unchanged;

                context.Devices.Add(device);
                context.SaveChanges();
            }
        }
    }
}