using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface IApplicationUserService
    {
        Iotshop.Models.ApplicationUser ApplicationUserByName(string name);
        void UpdateApplicationUser(Iotshop.Models.ApplicationUser user);
    }
}
