using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IApplicationUserRepository
    {
        Webshop.Models.ApplicationUser GetByName(object name);
    }
}
