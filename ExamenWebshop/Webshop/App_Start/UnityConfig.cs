using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Webshop.Controllers;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models.Models;
using Webshop.BusinessLayer.Services;
using System.Data.Entity;
using Webshop.BusinessLayer.Context;

namespace Webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<DbContext, ApplicationDbContext>(new PerRequestLifetimeManager());

            container.RegisterType<IDeviceRepository, DeviceRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IBasketItemRepository, BasketItemRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IApplicationUserRepository, ApplicationUserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new PerRequestLifetimeManager());

            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>(new PerRequestLifetimeManager());

            container.RegisterType<IDeviceService, DeviceService>(new PerRequestLifetimeManager());
            container.RegisterType<IBasketItemService, BasketItemService>(new PerRequestLifetimeManager());
            container.RegisterType<IApplicationUserService, ApplicationUserService>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderService, OrderService>(new PerRequestLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}