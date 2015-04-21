using System;
namespace Week2Oefening1.Models.DAL
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Week2Oefening1.Models.ApplicationUser GetByName(string name);
    }
}
