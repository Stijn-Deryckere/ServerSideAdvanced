using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models;

namespace Webshop.BusinessLayer.Services
{
    public class ApplicationUserService : Webshop.BusinessLayer.Services.IApplicationUserService
    {
        private ApplicationUserRepository ApplicatonUserRepo = null;

        public ApplicationUserService(ApplicationUserRepository applicationUserRepo)
        {
            this.ApplicatonUserRepo = applicationUserRepo;
        }

        public ApplicationUser ApplicationUserByName(String name)
        {
            return this.ApplicatonUserRepo.GetByName(name);
        }
    }
}
