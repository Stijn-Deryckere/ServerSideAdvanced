using System;
namespace Iotshop.BusinessLayer.Services
{
    public interface ILanguageService
    {
        System.Collections.Generic.IEnumerable<Iotshop.Models.AvailableCulture> AllAvailableCultures();
        Iotshop.Models.AvailableCulture AvailableCultureById(int id);
    }
}
