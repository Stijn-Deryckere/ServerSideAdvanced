using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.BusinessLayer.Cache
{
    public class WebshopCache
    {
        public static ConnectionMultiplexer connection = null;
        public static IDatabase cache = null;

        public static void Setup()
        {
            var config = new ConfigurationOptions();
            config.SyncTimeout = 5000;
            config.EndPoints.Add("iotshop.redis.cache.windows.net");
            config.Ssl = true;
            config.Password = "l60y6xlpHi5sCFnLJUNC397ALsFqGyPU+ZJvx6tcZhY=";

            connection = ConnectionMultiplexer.Connect(config);
            cache = connection.GetDatabase();
        }
    }
}
