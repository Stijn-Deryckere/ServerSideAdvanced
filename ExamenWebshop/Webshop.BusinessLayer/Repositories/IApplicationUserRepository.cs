using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IApplicationUserRepository
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.Models.ApplicationUser> All();
        Webshop.Models.Models.ApplicationUser GetByID(object id);
        Webshop.Models.Models.ApplicationUser GetByName(string name);
        void Update(Webshop.Models.Models.ApplicationUser entityToUpdate);
    }
}
