using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Webshop.Models;
using Webshop.BusinessLayer.Context;

namespace Webshop.BusinessLayer.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, Webshop.BusinessLayer.Repositories.IApplicationUserRepository
    {
        public ApplicationUserRepository()
        {

        }

        public ApplicationUserRepository(WebshopContext context)
            :base(context)
        {

        }

        public ApplicationUser GetByName(Object name)
        {
            var query = this.context.Users.Where(u => u.UserName == name.ToString());
            return query.SingleOrDefault<ApplicationUser>();
        }

        public override void Update(ApplicationUser entityToUpdate)
        {
            base.Update(entityToUpdate);
        }
    }
}
