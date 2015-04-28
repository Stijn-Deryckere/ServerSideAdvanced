using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, Webshop.BusinessLayer.Repositories.IBasketItemRepository
    {
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
            var query = from b in this.context.BasketItems.Include(d => d.NewDevice).Include(u => u.NewUser) where b.NewUser.UserName == user.UserName select b;
            return query;
        }

        public override BasketItem Insert(BasketItem entity)
        {
            this.context.Entry<Device>(entity.NewDevice).State = EntityState.Unchanged;
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
    }
}
