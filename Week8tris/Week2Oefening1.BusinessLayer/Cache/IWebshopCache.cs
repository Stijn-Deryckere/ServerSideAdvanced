using System;
namespace Week2Oefening1.BusinessLayer.Cache
{
    public interface IWebshopCache
    {
        void DecrementBasketItemsAmount(Week2Oefening1.Models.ApplicationUser user);
        System.Collections.Generic.List<Week2Oefening1.Models.Device> GetDevicesFromCache();
        System.Collections.Generic.List<Week2Oefening1.Models.Framework> GetFrameworksFromCache();
        int GetNumberOfBasketItemsOfUser(Week2Oefening1.Models.ApplicationUser user);
        System.Collections.Generic.List<Week2Oefening1.Models.OS> GetOSsFromCache();
        void IncrementBasketItemsAmount(Week2Oefening1.Models.ApplicationUser user);
        void RefreshCacheDevices();
        void RefreshCachedFrameworks();
        void RefreshCacheOSs();
    }
}
