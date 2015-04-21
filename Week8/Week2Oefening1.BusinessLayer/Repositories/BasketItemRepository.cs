using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Week2Oefening1.BusinessLayer.Context;

namespace Week2Oefening1.Models.DAL
{
    public class BasketItemRepository : GenericRepository<BasketItem>, Week2Oefening1.BusinessLayer.Repositories.IBasketItemRepository
    {
        public BasketItemRepository() 
        { }
        
        public BasketItemRepository(ApplicationDbContext context) : base(context) 
        { }

        public override IEnumerable<BasketItem> All()
        {
            return this.context.BasketItems.AsNoTracking<BasketItem>().Include(b => b.RentDevice).Include(b => b.RentUser);
        }

        public override BasketItem Insert(BasketItem entity)
        {
            this.context.Entry<ApplicationUser>(entity.RentUser).State = EntityState.Unchanged;
            this.context.Entry<Device>(entity.RentDevice).State = EntityState.Unchanged;

            this.context.BasketItems.Add(entity);
            this.context.SaveChanges();
            return entity;
        }

        public IEnumerable<BasketItem> AllOfUser(String id)
        {
            var query = from b in this.context.BasketItems.Include(d => d.RentDevice).Include(u => u.RentUser) where b.RentUser.Id == id && b.IsDeleted == false select b;
            return query.ToList<BasketItem>();
        }

        public override void Delete(BasketItem entityToDelete)
        {
            this.context.Entry<ApplicationUser>(entityToDelete.RentUser).State = EntityState.Unchanged;
            this.context.Entry<Device>(entityToDelete.RentDevice).State = EntityState.Unchanged;
            this.context.Entry<BasketItem>(entityToDelete).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public override void Update(BasketItem entityToUpdate)
        {
            this.context.Entry<ApplicationUser>(entityToUpdate.RentUser).State = EntityState.Unchanged;
            this.context.Entry<Device>(entityToUpdate.RentDevice).State = EntityState.Unchanged;
            this.context.Entry<BasketItem>(entityToUpdate).State = EntityState.Modified;            
        }

        public override void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public BasketItem AllOfUserAndDevice(ApplicationUser user, Device device)
        {
            var query = from b in this.context.BasketItems.AsNoTracking<BasketItem>().Include(d => d.RentDevice).Include(u => u.RentUser) where b.RentUser.Id == user.Id && b.IsDeleted == false && b.RentDevice.Id == device.Id select b;
            return query.SingleOrDefault<BasketItem>();
        }
    }
}