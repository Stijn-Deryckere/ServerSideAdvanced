using System;
namespace Iotshop.BusinessLayer.Repositories
{
    public interface IApplicationUserRepository
    {
        Iotshop.Models.ApplicationUser GetByName(object name);
        void Update(Iotshop.Models.ApplicationUser entityToUpdate);
    }
}
