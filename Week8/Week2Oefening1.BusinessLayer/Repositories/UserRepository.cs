using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Week2Oefening1.BusinessLayer.Context;

namespace Week2Oefening1.Models.DAL
{
    public class UserRepository : GenericRepository<ApplicationUser>, Week2Oefening1.Models.DAL.IUserRepository
    {
        public UserRepository()
        { }

        public UserRepository(ApplicationDbContext context) : base(context)
        { }

        public ApplicationUser GetByName(String name)
        {
            var query = this.context.Users.AsNoTracking<ApplicationUser>().Where(u => u.UserName == name);
            return query.SingleOrDefault<ApplicationUser>();
        }
    }
}