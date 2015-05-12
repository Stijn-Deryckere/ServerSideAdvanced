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
    public class BasketItemRepository : GenericRepository<BasketItem>, Webshop.BusinessLayer.Repositories.IBasketItemRepository
    {
        public BasketItemRepository()
        {

        }

        public BasketItemRepository(ApplicationDbContext context)
            :base(context)
        {

        }

        public IEnumerable<BasketItem> GetByUser(ApplicationUser user)
        {
            return this.context.BasketItems.Include(u => u.NewUser).Include(d => d.NewDevice).Include(d => d.NewDevice.DeviceOSs).Include(d => d.NewDevice.DeviceFrameworks).Where(u => u.NewUser.Id == user.Id).Where(i => i.IsActive == true);
        }

        public override BasketItem Insert(BasketItem entity)
        {
            this.context.Entry<ApplicationUser>(entity.NewUser).State = EntityState.Unchanged;
            this.context.Entry<Device>(entity.NewDevice).State = EntityState.Unchanged;
            foreach(OS os in entity.NewDevice.DeviceOSs)
            {
                this.context.Entry<OS>(os).State = EntityState.Unchanged;
            }

            foreach(Framework framework in entity.NewDevice.DeviceFrameworks)
            {
                this.context.Entry<Framework>(framework).State = EntityState.Unchanged;
            }

            this.context.BasketItems.Add(entity);
            SaveChanges();

            return entity;
        }

        public override void Delete(BasketItem entityToDelete)
        {
            entityToDelete.IsActive = false;
            this.context.Entry<BasketItem>(entityToDelete).State = EntityState.Modified;
            SaveChanges();
        }
    }
}
