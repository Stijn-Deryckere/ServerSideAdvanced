using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.Models.Services;
using Week2Oefening1.Controllers;
using Week2Oefening1.BusinessLayer.Services;
using Week2Oefening1.BusinessLayer.Repositories;
using Week2Oefening1.BusinessLayer.Cache;

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
            container.RegisterType<IBasketItemRepository, BasketItemRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<IBasketItemService, BasketItemService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IWebshopCache, WebshopCache>();


            container.RegisterType<AccountController>(new InjectionConstructor());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}