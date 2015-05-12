using System;
namespace Language.BusinessLayer.Services
{
    public interface ICultureService
    {
        System.Collections.Generic.IEnumerable<Language.Models.Models.AvailableCulture> AllCultures();
        Language.Models.Models.AvailableCulture CultureById(int id);
    }
}
