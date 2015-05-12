using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;

namespace Webshop.BusinessLayer.Services
{
    public class ApplicationUserService : Webshop.BusinessLayer.Services.IApplicationUserService
    {
        private IApplicationUserRepository ApplicationUserRepo = null;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepo)
        {
            this.ApplicationUserRepo = applicationUserRepo;
        }

        public IEnumerable<ApplicationUser> AllApplicationUsers()
        {
            return this.ApplicationUserRepo.All();
        }

        public ApplicationUser ApplicationUserById(int id)
        {
            return this.ApplicationUserRepo.GetByID(id);
        }

        public ApplicationUser ApplicationUserByName(String name)
        {
            return this.ApplicationUserRepo.GetByName(name);
        }

        public void UpdateApplicationUser(ApplicationUser user)
        {
            this.ApplicationUserRepo.Update(user);
        }
    }
}
