using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iotshop.BusinessLayer.Repositories;
using Iotshop.Models;

namespace Iotshop.BusinessLayer.Services
{
    public class ApplicationUserService : Iotshop.BusinessLayer.Services.IApplicationUserService
    {
        private IApplicationUserRepository ApplicatonUserRepo = null;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepo)
        {
            this.ApplicatonUserRepo = applicationUserRepo;
        }

        public ApplicationUser ApplicationUserByName(String name)
        {
            return this.ApplicatonUserRepo.GetByName(name);
        }

        public void UpdateApplicationUser(ApplicationUser user)
        {
            this.ApplicatonUserRepo.Update(user);
        }
    }
}
