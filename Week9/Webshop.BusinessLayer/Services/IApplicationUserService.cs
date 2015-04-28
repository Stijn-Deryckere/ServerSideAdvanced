using System;
namespace Webshop.BusinessLayer.Services
{
    public interface IApplicationUserService
    {
        Webshop.Models.ApplicationUser ApplicationUserByName(string name);
    }
}
