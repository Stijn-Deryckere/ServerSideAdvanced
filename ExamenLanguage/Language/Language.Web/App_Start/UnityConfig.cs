using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Language.Web.Controllers;
using Language.BusinessLayer.Repositories;
using Language.Models.Models;
using System.Data.Entity;
using Language.BusinessLayer.Context;
using Language.BusinessLayer.Services;

namespace Language.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext, ApplicationDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<IGenericRepository<Device>, GenericRepository<Device>>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<AvailableCulture>, GenericRepository<AvailableCulture>>(new PerRequestLifetimeManager());

            container.RegisterType<IDeviceService, DeviceService>(new PerRequestLifetimeManager());
            container.RegisterType<ICultureService, CultureService>(new PerRequestLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}