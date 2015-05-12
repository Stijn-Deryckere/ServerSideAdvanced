# Redis Cache

## Stap 1: NuGet Package toevoegen

Voeg het NuGet Package **StackExchange.Redis** toe aan de businesslayer.

## Stap 2: Maak connectie met Redis Cache

* Maak in de **BusinessLayer** een map **Cache** en plaats aarin het bestand **WebsiteCache.cs**.
* Voorzie de zojuist aangemaakte klasse van een statische Setup()-methode:

```
public static ConnectionMultiplexer connection = null;
public static IDatabase cache = null;

public static void Setup()
{
	var config = new ConfigurationOptions();
    config.SyncTimeout = 5000;
    config.EndPoints.Add("iotshop.redis.cache.windows.net");
    config.Ssl = true;
    config.Password = "primary key";

    connection = ConnectionMultiplexer.Connect(config);
    cache = connection.GetDatabase();
}
```

* Roep de bovenstaande methode nu op in Global.asax, zodat er vanaf het opstarten van de applicatie connectie gemaakt wordt met de Redis Cache.

```
protected void Application_Start()
{
	AreaRegistration.RegisterAllAreas();
    GlobalConfiguration.Configure(WebApiConfig.Register);
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
    UnityConfig.RegisterComponents();
    WebshopCache.Setup();
}
```

## Stap 3: SampleStackExchangeRedisExtensions

Importeer nu de klasse SampleStackExchangeRedisExtensions naar de **Cache-map**:

```
public static class SampleStackExchangeRedisExtensions {
        public static T Get<T>(this IDatabase cache, string key) {
            return Deserialize<T>(cache.StringGet(key));
        }

        public static object Get(this IDatabase cache, string key) {
            return Deserialize<object>(cache.StringGet(key));
        }

        public static void Set(this IDatabase cache, string key, object value) {
            cache.StringSet(key, Serialize(value));
        }

        static byte[] Serialize(object o) {
            if (o == null) {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream()) {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        static T Deserialize<T>(byte[] stream) {
            if (stream == null) {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream)) {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
```

## Stap 4: Markeer objecten als Serialiseerbaar

Vergeet nu niet om alle klassen die we wensen te serialiseren als [Serializable] te markeren.

## Stap 5: Zaken uit de cache ophalen of toevoegen, via cache-aside

We hanteren hiervoor het cache-aside-principe. Vergeet bij het Refreshen overigens niet de ToList<>()-methode op te roepen. Doe je dan niet, dan stuur je een onuitgevoerde query naar de cache.

```
public List<Device> GetDevicesFromCache()
{
	List<Device> devices = WebshopCache.cache.Get("Devices") as List<Device>();
    if(devices != null)
    	return devices;

    RefreshCachedDevices();
}

public void RefreshCachedDevices()
{
	IEnumerable<Device> devices = this.DeviceRepo.All()ToList<Device>();
    WebshopCache.cache.Set("Devices", devices);
}
```

Roep nu ook regelmatig RefreshCachedDevices() op om ervoor te zorgen dat de cache up-to-date blijft.

## Stap 6: Voorbeeld van het incrementen van een getal in de cache

```
public int CountItemsInBasket(ApplicationUser user)
{
	String userKey = user.UserName + ";Count";
    String itemsCount = WebshopCache.cache.StringGet(userKey);
    if (itemsCount != null)
    {
    	int intItemsCount = 0;
        Boolean test = Int32.TryParse(itemsCount, out intItemsCount);

        return Convert.ToInt32(intItemsCount);
    }
                

    RefreshItemsCount(user);

    return CountItemsInBasket(user);
}

public void RefreshItemsCount(ApplicationUser user)
{
	String userKey = user.UserName + ";Count";
    int itemsCount = this.BasketItemRepo.GetByUser(user).Count();
    WebshopCache.cache.Set(userKey, itemsCount);
}

public void IncrementItemsCount(ApplicationUser user)
{
	String userKey = user.UserName + ";Count";
    WebshopCache.cache.StringIncrement(userKey, 1);
}
```