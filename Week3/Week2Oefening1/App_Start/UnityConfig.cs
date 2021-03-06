using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.Models.Services;

namespace Week2Oefening1
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>();
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IDeviceService, DeviceService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}