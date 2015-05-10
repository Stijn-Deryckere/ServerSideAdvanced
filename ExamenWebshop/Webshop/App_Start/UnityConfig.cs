using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Webshop.Controllers;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;
using Webshop.BusinessLayer.Services;

namespace Webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IDeviceRepository, DeviceRepository>(new PerRequestLifetimeManager());

            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>(new PerRequestLifetimeManager());

            container.RegisterType<IDeviceService, DeviceService>(new PerRequestLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}