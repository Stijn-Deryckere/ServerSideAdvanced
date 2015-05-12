using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IApplicationUserService
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.ApplicationUser> AllApplicationUsers();
        Webshop.Models.Models.ApplicationUser ApplicationUserById(int id);
        Webshop.Models.Models.ApplicationUser ApplicationUserByName(string name);
        void UpdateApplicationUser(Webshop.Models.Models.ApplicationUser user);
    }
}
