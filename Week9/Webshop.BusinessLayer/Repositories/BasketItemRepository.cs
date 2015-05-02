using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, Webshop.BusinessLayer.Repositories.IBasketItemRepository
    {
        public BasketItemRepository()
        { }

        public BasketItemRepository(WebshopContext context)
            :base(context)
        { }

        public override IEnumerable<BasketItem> All()
        {
            return base.All();
        }

        public override BasketItem GetByID(object id)
        {
            return this.context.BasketItems.Include(u => u.NewUser).Include(d => d.NewDevice).Where(b => b.ID == (int)id).SingleOrDefault<BasketItem>();
        }

        public IEnumerable<BasketItem> GetByUser(ApplicationUser user)
        {
            var query = from b in this.context.BasketItems
                            .Include(d => d.NewDevice)
                            .Include(o => o.NewDevice.DeviceOS)
                            .Include(f => f.NewDevice.DeviceFramework)
                            .Include(u => u.NewUser) 
                        where b.NewUser.UserName == user.UserName select b;
            return query;
        }

        public IEnumerable<BasketItem> GetByVisitorGUID(String visitorGUID)
        {
            var query = from b in this.context.BasketItems
                            .Include(d => d.NewDevice)
                            .Include(o => o.NewDevice.DeviceOS)
                            .Include(f => f.NewDevice.DeviceFramework)
                            .Include(u => u.NewUser)
                        where b.visitorGUID == visitorGUID select b;
            return query;
        }

        public override BasketItem Insert(BasketItem entity)
        {
            this.context.Entry<Device>(entity.NewDevice).State = EntityState.Unchanged;
            if(entity.NewUser != null)
                this.context.Entry<ApplicationUser>(entity.NewUser).State = EntityState.Unchanged;
            this.context.BasketItems.Add(entity);
            this.context.SaveChanges();

            return entity;
        }

        public override void Update(BasketItem entityToUpdate)
        {
            this.context.Entry<Device>(entityToUpdate.NewDevice).State = EntityState.Unchanged;
            this.context.Entry<ApplicationUser>(entityToUpdate.NewUser).State = EntityState.Unchanged;
            this.context.SaveChanges();
        }

        public void UpdateUser(BasketItem basketItem)
        {
            this.context.Entry<Device>(basketItem.NewDevice).State = EntityState.Unchanged;
            this.context.Entry<ApplicationUser>(basketItem.NewUser).State = EntityState.Modified;
            this.context.Entry<BasketItem>(basketItem).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public override void Delete(object id)
        {
            BasketItem basketItem = this.GetByID(id);
            this.context.Entry<Device>(basketItem.NewDevice).State = EntityState.Unchanged;
            this.context.Entry<ApplicationUser>(basketItem.NewUser).State = EntityState.Unchanged;
            this.context.Entry<BasketItem>(basketItem).State = EntityState.Deleted;
            this.SaveChanges();
        }
    }
}
