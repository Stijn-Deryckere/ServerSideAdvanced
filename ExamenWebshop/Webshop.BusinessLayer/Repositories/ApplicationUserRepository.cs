using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, Webshop.BusinessLayer.Repositories.IApplicationUserRepository
    {
        public ApplicationUserRepository()
        {

        }

        public ApplicationUserRepository(ApplicationDbContext context)
            :base(context)
        {

        }

        public override IEnumerable<ApplicationUser> All()
        {
            return base.All();
        }

        public override ApplicationUser GetByID(object id)
        {
            return base.GetByID(id);
        }

        public ApplicationUser GetByName(String name)
        {
            return this.context.Users.Where(u => u.UserName == name).SingleOrDefault<ApplicationUser>();
        }

        public override void Update(ApplicationUser entityToUpdate)
        {
            this.context.Entry<ApplicationUser>(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            SaveChanges();
        }
    }
}
