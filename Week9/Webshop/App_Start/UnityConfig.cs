using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Context;
using Webshop.BusinessLayer.Services;
using Webshop.Controllers;
using Webshop.Models;

namespace Webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext, WebshopContext>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>(new PerRequestLifetimeManager());
            container.RegisterType<IDeviceRepository, DeviceRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IBasketItemRepository, BasketItemRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IApplicationUserRepository, ApplicationUserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<AvailableCulture>, GenericRepository<AvailableCulture>>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IGenericRepository<FormTopic>, GenericRepository<FormTopic>>(new PerRequestLifetimeManager());
            container.RegisterType<IFormRepository, FormRepository>(new PerRequestLifetimeManager());

            container.RegisterType<IDeviceService, DeviceService>(new PerRequestLifetimeManager());
            container.RegisterType<IBasketItemService, BasketItemService>(new PerRequestLifetimeManager());
            container.RegisterType<IApplicationUserService, ApplicationUserService>(new PerRequestLifetimeManager());
            container.RegisterType<ILanguageService, LanguageService>(new PerRequestLifetimeManager());
            container.RegisterType<IOrderService, OrderService>(new PerRequestLifetimeManager());
            container.RegisterType<IFormService, FormService>(new PerRequestLifetimeManager());

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}