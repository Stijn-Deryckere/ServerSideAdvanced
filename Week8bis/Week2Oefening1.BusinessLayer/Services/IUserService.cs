using System;
namespace Week2Oefening1.Models.Services
{
    public interface IUserService
    {
        Week2Oefening1.Models.ApplicationUser UserByName(string name);
    }
}
