using System;
namespace Webshop.BusinessLayer.Services
{
    public interface ILanguageService
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.AvailableCulture> AllAvailableCultures();
        Webshop.Models.AvailableCulture AvailableCultureById(int id);
    }
}
