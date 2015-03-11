using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Week2Oefening1.Controllers;
using Week2Oefening1.Models;
using Week2Oefening1.Models.DAL;
using Week2Oefening1.Models.Services;
using Week2Oefening1.Test.Database;
using Week2Oefening1.ViewModels;

namespace Week2Oefening1.Test.IntegrationTests
{
    [TestClass]
    public class CatalogController_Test
    {
        private CatalogController controller = null;
        private IDeviceService deviceService = null;
        private IGenericRepository<OS> repoOS = null;
        private IGenericRepository<Framework> repoFramework = null;
        private IDeviceRepository repoDevice = null;
        private IBasketItemRepository repoBasketItem = null;
        private IBasketItemService basketItemService = null;

        [TestInitialize]
        public void Setup()
        {
            new SetupDatabase().InitializeDatabase(new ApplicationDbContext());

            repoOS = new GenericRepository<OS>();
            repoFramework = new GenericRepository<Framework>();
            repoDevice = new DeviceRepository();
            repoBasketItem = new BasketItemRepository();
            deviceService = new DeviceService(repoOS, repoFramework, repoDevice);
            basketItemService = new BasketItemService(repoBasketItem);
            controller = new CatalogController(deviceService, basketItemService);
        }

        [TestMethod]
        public void Index_Test()
        {
            //Act 
            ViewResult vr = (ViewResult) controller.Index();
            List<Device> devices = vr.Model as List<Device>;

            //Assert
            Assert.IsNotNull(vr);
            Assert.IsInstanceOfType(vr.Model, typeof(List<Device>));
            Assert.IsTrue(devices.Count == 5);
        }

        [TestMethod]
        public void Detail_Test()
        {
            //Act
            ViewResult vr = (ViewResult)controller.Detail(1);
            BasketItemVM basketItemVM = (BasketItemVM)vr.Model;

            //Assert
            Assert.IsNotNull(vr);
            Assert.IsInstanceOfType(vr.Model, typeof(BasketItemVM));
            Assert.IsTrue(basketItemVM.NewDevice.Equals(deviceService.DeviceById(1)));
        }

        [TestMethod]
        public void Add_Test()
        {
            //Act
            ViewResult vr = (ViewResult)controller.Add();
            DeviceVM dvm = (DeviceVM)vr.Model;

            //Assert
            Assert.IsNotNull(vr);
            Assert.IsInstanceOfType(dvm, typeof(DeviceVM));
            Assert.IsTrue(dvm.OperatingSystems.Count() == 4);
        }

        [TestMethod]
        public void Price_Test()
        {
            //Act
            BasketItem basketItem1 = new BasketItem()
            {
                Amount = 2,
                IsDeleted = false,
                RentDevice = deviceService.DeviceById(2),
                RentUser = new ApplicationUser() { Id = "1" },
                Timestamp = DateTime.Now
            };

            BasketItem basketItem2 = new BasketItem()
            {
                Amount = 3,
                IsDeleted = false,
                RentDevice = deviceService.DeviceById(3),
                RentUser = new ApplicationUser() { Id = "1" },
                Timestamp = DateTime.Now
            };

            List<BasketItem> basketItems = new List<BasketItem>();
            basketItems.Add(basketItem1);
            basketItems.Add(basketItem2);

            ItemCountPrice basketItemCountPrice = basketItemService.GetBasketItemCountPrice(basketItems);
            double totalPrice = basketItemCountPrice.TotalPrice;

            double countPrice = (deviceService.DeviceById(2).RentingPrice * 2) + (deviceService.DeviceById(3).RentingPrice * 3);

            //Assert
            Assert.IsTrue(totalPrice == countPrice);
        }
    }
}
