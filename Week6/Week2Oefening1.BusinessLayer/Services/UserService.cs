using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week2Oefening1.Models.DAL;

namespace Week2Oefening1.Models.Services
{
    public class UserService : Week2Oefening1.Models.Services.IUserService
    {
        private IUserRepository repoUser = null;

        public UserService(IUserRepository repoUser)
        {
            this.repoUser = repoUser;
        }

        /*
         * Users
         */
        public ApplicationUser UserByName(String name)
        {
            return this.repoUser.GetByName(name);
        }
    }
}