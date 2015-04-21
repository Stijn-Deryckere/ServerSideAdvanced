using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Webshop.Models;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Services;
using Webshop.Controllers;

namespace Webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();
            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();

            container.RegisterType<IDeviceService, DeviceService>();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}