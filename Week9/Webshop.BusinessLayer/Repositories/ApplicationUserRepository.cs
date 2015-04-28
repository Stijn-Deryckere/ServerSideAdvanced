using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Webshop.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, Webshop.BusinessLayer.Repositories.IApplicationUserRepository
    {
        public ApplicationUser GetByName(Object name)
        {
            var query = this.context.Users.AsNoTracking().Where(u => u.UserName == name.ToString());
            return query.SingleOrDefault<ApplicationUser>();
        }
    }
}
