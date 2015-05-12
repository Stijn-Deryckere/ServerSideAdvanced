using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Webshop.BusinessLayer.Context;
using Webshop.BusinessLayer.Repositories;
using Webshop.BusinessLayer.Services;
using Webshop.Controllers;
using Webshop.Models.Models;
using Webshop.Models.PresentationModels;
using Webshop.Test.Database;

namespace Webshop.Test.IntegrationTests
{
    [TestClass]
    public class CatalogController_Test
    {
        private CatalogController catalogController = null;
        private IDeviceService deviceServ = null;
        private IDeviceRepository deviceRepo = null;
        private IGenericRepository<OS> oSRepo = null;
        private IGenericRepository<Framework> frameworkRepo = null;

        [TestInitialize]
        public void Setup()
        {
            new UnitTestDatabaseInitializer().InitializeDatabase(new ApplicationDbContext());

            this.deviceRepo = new DeviceRepository();
            this.oSRepo = new GenericRepository<OS>();
            this.frameworkRepo = new GenericRepository<Framework>();
            this.deviceServ = new DeviceService(this.deviceRepo, this.oSRepo, this.frameworkRepo);

            this.catalogController = new CatalogController(deviceServ);
        }

        [TestMethod]
        public void Index_Test()
        {
            //Arrange

            //Act
            ViewResult viewResult = (ViewResult) this.catalogController.Index();
            List<Device> devices = (List<Device>) viewResult.Model;

            //Assert
            Assert.IsNotNull(viewResult);
            Assert.IsTrue(devices.Count() == 5);
            Assert.IsInstanceOfType(viewResult.Model, typeof(List<Device>));
        }

        [TestMethod]
        public void Detail_Test()
        {
            //Arrange

            //Act
            ViewResult viewResult = (ViewResult)this.catalogController.Detail(1);
            Device device = (Device) viewResult.Model;

            //Assert
            Device resultDevice = this.deviceServ.DeviceById(1);
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(device);
            Assert.AreEqual(resultDevice, device);
        }

        [TestMethod]
        public void Add_Test()
        {
            //Arrange 

            //Act
            ViewResult viewResult = (ViewResult) this.catalogController.Add();
            DevicePM devicePM = (DevicePM) viewResult.Model;

            //Assert
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(devicePM);
            Assert.IsTrue(devicePM.NewDevice.DeviceFrameworks.Count() == 5);
            Assert.IsTrue(devicePM.NewDevice.DeviceOSs.Count() == 5);
        }
    }
}
