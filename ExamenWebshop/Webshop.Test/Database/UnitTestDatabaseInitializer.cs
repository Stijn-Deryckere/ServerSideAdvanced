using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models.Models;

namespace Webshop.Test.Database
{
    public class UnitTestDatabaseInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            List<OS> oSs = CreateOSs().ToList<OS>();
            foreach(OS os in oSs)
            {
                context.OSs.Add(os);
            }

            List<Framework> frameworks = CreateFrameworks().ToList<Framework>();
            foreach(Framework framework in frameworks)
            {
                context.Frameworks.Add(framework);
            }

            CreateDevices(context);

            context.SaveChanges();
        }

        private IEnumerable<OS> CreateOSs()
        {
            List<OS> oSs = new List<OS>();
            for(int i = 0; i < 5; i++)
            {
                OS os = new OS()
                {
                    ID = i,
                    Name = "OS" + i
                };
                oSs.Add(os);
            }

            return oSs;
        }

        private IEnumerable<Framework> CreateFrameworks()
        {
            List<Framework> frameworks = new List<Framework>();
            for(int i = 0; i < 5; i++)
            {
                Framework framework = new Framework()
                {
                    ID = i,
                    Name = "Framework" + i
                };
                frameworks.Add(framework);
            }

            return frameworks;
        }
        
        private void CreateDevices(ApplicationDbContext context)
        {
            for(int i = 0; i < 5; i++)
            {
                Device device = new Device()
                {
                    ID = i,
                    Name = "Device" + i,
                    Description = "Device" + i,
                    Picture = "Device" + i + ".jpg",
                    PurchasePrice = 10.0,
                    RentPrice = 10.0,
                    Stock = 100,
                    DeviceOSs = new List<OS>(),
                    DeviceFrameworks = new List<Framework>()
                };

                device.DeviceOSs.Add(context.OSs.Find(i));
                device.DeviceFrameworks.Add(context.Frameworks.Find(i));

                context.Devices.Add(device);
            }
        }
    }
}
