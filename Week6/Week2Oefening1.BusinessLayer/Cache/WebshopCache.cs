using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.BusinessLayer.Context;
using Week2Oefening1.Models.Services;
using Week2Oefening1.BusinessLayer.Repositories;

namespace Week2Oefening1.BusinessLayer.Cache
{
    public class WebshopCache : Week2Oefening1.BusinessLayer.Cache.IWebshopCache
    {
        private static ConnectionMultiplexer connection = null;
        private static IDatabase cache = null;

        private IDeviceRepository deviceRepo = null;
        private IGenericRepository<OS> osRepo = null;
        private IGenericRepository<Framework> frameworkRepo = null;
        private IBasketItemRepository basketItemRepo = null;

        public WebshopCache(IDeviceRepository deviceRepo, IGenericRepository<OS> osRepo, IGenericRepository<Framework> frameworkRepo, IBasketItemRepository basketItemRepo)
        {
            this.deviceRepo = deviceRepo;
            this.osRepo = osRepo;
            this.frameworkRepo = frameworkRepo;
            this.basketItemRepo = basketItemRepo;
        }

        public static void Setup()
        {
            /*
             * Connectie met Redis Cache instellen.
             */
            var config = new ConfigurationOptions();
            config.SyncTimeout = 5000;
            config.EndPoints.Add("internetofthingsshop.redis.cache.windows.net");
            config.Ssl = true;
            config.Password = "D4qyynuesiB0QTFczmyNlsW/EYeDW7RkVIQtodBVNIk=";

            connection = ConnectionMultiplexer.Connect(config);
            cache = connection.GetDatabase();
        }

        /*
         * GET
         */
        public List<Device> GetDevicesFromCache()
        {
            List<Device> cachedDevices = cache.Get("Devices") as List<Device>;
            if (cachedDevices != null)
                return cachedDevices;

            RefreshCacheDevices();

            return cache.Get("Devices") as List<Device>;
        }

        public List<OS> GetOSsFromCache()
        {
            List<OS> cachedOSs = cache.Get("OSs") as List<OS>;
            if (cachedOSs != null)
                return cachedOSs;

            RefreshCacheOSs();

            return cache.Get("OSs") as List<OS>;
        }

        public List<Framework> GetFrameworksFromCache()
        {
            List<Framework> cachedFrameworks = cache.Get("Frameworks") as List<Framework>;
            if (cachedFrameworks != null)
                return cachedFrameworks;

            RefreshCachedFrameworks();

            return cache.Get("Frameworks") as List<Framework>;
        }

        public int GetNumberOfBasketItemsOfUser(ApplicationUser user)
        {
            String cachedBasketItemAmount = cache.Get("basketAmount;" + user.UserName) as String;
            if (cachedBasketItemAmount != null)
                return Convert.ToInt32(cachedBasketItemAmount);

            RefreshCachedBasketItemAmounts(user);

            return Convert.ToInt32(cache.Get("basketAmount;" + user.UserName) as String);
        }

        /*
         * Refresh
         */
        public void RefreshCacheDevices()
        {
            IEnumerable<Device> devices = deviceRepo.All().ToList<Device>();
            cache.Set("Devices", devices);
        }

        public void RefreshCacheOSs()
        {
            IEnumerable<OS> oss = osRepo.All().ToList<OS>();
            cache.Set("OSs", oss);
        }

        public void RefreshCachedFrameworks()
        {
            IEnumerable<Framework> frameworks = frameworkRepo.All().ToList<Framework>();
            cache.Set("Frameworks", frameworks);
        }

        private void RefreshCachedBasketItemAmounts(ApplicationUser user)
        {
            IEnumerable<BasketItem> basketItems = basketItemRepo.AllOfUser(user.Id).ToList<BasketItem>();
            String amount = "" + basketItems.Count();

            cache.Set("basketAmount;" + user.UserName, amount);
        }

        /*
         * Others
         */
        public void IncrementBasketItemsAmount(ApplicationUser user)
        {
            int amount = Convert.ToInt32(cache.Get("basketAmount;" + user.UserName));
            amount++;
            cache.Set("basketAmount;" + user.UserName, amount);
        }
    }
}
