using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2Oefening1.BusinessLayer.Context;
using Week2Oefening1.Models;

namespace Week2Oefening1.Test.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            List<OS> operatingSystems = ReadOperatingSystems();
            List<Framework> frameworks = ReadFrameworks();

            foreach (OS os in operatingSystems)
                context.OperatingSystems.Add(os);

            foreach (Framework fw in frameworks)
                context.Frameworks.Add(fw);

            List<Device> devices = ReadDevices(context);
            foreach (Device d in devices)
                context.Devices.Add(d);

            AddRoles(context);

            context.SaveChanges();
        }

        private List<OS> ReadOperatingSystems()
        {
            String filepath = AppDomain.CurrentDomain.BaseDirectory + "/../Debug/Os.txt";
            StreamReader osr = new StreamReader(filepath);
            osr.ReadLine();

            List<OS> operatingSystems = new List<OS>();
            String line = osr.ReadLine();
            while (line != null)
            {
                String[] parts = line.Split(';');
                OS operatingSystem = new OS()
                {
                    Name = parts[1]
                };
                operatingSystems.Add(operatingSystem);

                line = osr.ReadLine();
            }
            return operatingSystems;
        }

        private List<Framework> ReadFrameworks()
        {
            String filepath = AppDomain.CurrentDomain.BaseDirectory + "/../Debug/ProgrammingFramework.txt";
            StreamReader osr = new StreamReader(filepath);
            osr.ReadLine();

            List<Framework> frameworks = new List<Framework>();
            String line = osr.ReadLine();
            while (line != null)
            {
                String[] parts = line.Split(';');
                Framework framework = new Framework()
                {
                    Name = parts[1]
                };
                frameworks.Add(framework);

                line = osr.ReadLine();
            }
            return frameworks;
        }

        private List<Device> ReadDevices(ApplicationDbContext context)
        {
            String filepath = AppDomain.CurrentDomain.BaseDirectory + "/../Debug/Devices.txt";
            StreamReader osr = new StreamReader(filepath);
            osr.ReadLine();

            List<Device> devices = new List<Device>();
            String line = osr.ReadLine();
            while (line != null)
            {
                String[] parts = line.Split(';');
                Device device = new Device()
                {
                    Name = parts[1],
                    PurchasePrice = Convert.ToDouble(parts[2]),
                    RentingPrice = Convert.ToDouble(parts[3]),
                    Stock = Convert.ToInt32(parts[4]),
                    Image = parts[5],
                    Description = parts[8],
                    DeviceOS = GetDeviceOS(context, parts[6]),
                    DeviceFramework = GetFrameworkOS(context, parts[7])
                };
                devices.Add(device);
                line = osr.ReadLine();
            }
            return devices;
        }

        private List<OS> GetDeviceOS(ApplicationDbContext context, String part)
        {
            String[] parts = part.Split('-');
            List<OS> operatingSystems = new List<OS>();

            foreach (String tempPart in parts)
            {
                int tempId = Convert.ToInt32(tempPart);
                var query = (from o in context.OperatingSystems where o.Id == tempId select o);
                operatingSystems.Add(query.SingleOrDefault<OS>());
            }
            return operatingSystems;
        }

        private List<Framework> GetFrameworkOS(ApplicationDbContext context, String part)
        {
            String[] parts = part.Split('-');
            List<Framework> frameworks = new List<Framework>();

            foreach (String tempPart in parts)
            {
                int tempId = Convert.ToInt32(tempPart);
                var query = (from f in context.Frameworks where f.Id == tempId select f);
                frameworks.Add(query.SingleOrDefault<Framework>());
            }
            return frameworks;
        }

        private void AddRoles(ApplicationDbContext context)
        {
            String adminRole = "Administrator";
            String userRole = "User";

            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(adminRole))
                roleResult = roleManager.Create(new IdentityRole(adminRole));

            if (!roleManager.RoleExists(userRole))
                roleResult = roleManager.Create(new IdentityRole(userRole));

            if (!context.Users.Any(u => u.Email.Equals("dieter.de.preeter@howest.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "De Preester",
                    FirstName = "Dieter",
                    Email = "dieter.de.preester@howest.be",
                    UserName = "dieter.de.preester@howest.be",
                    Address = "Graaf Karel De Goedelaan 1",
                    City = "Kortrijk",
                    Country = "Belgium",
                    Zipcode = "8500"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, adminRole);
            }

            if (!context.Users.Any(u => u.Email.Equals("kristof@kristofcolpaert.com")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Colpaert",
                    FirstName = "Kristof",
                    Email = "kristof@kristofcolpaert.com",
                    UserName = "kristof@kristofcolpaert.com",
                    Address = "Zwalmkouter 12",
                    City = "Erembodegem",
                    Country = "Belgium",
                    Zipcode = "9320"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, adminRole);
            }

            if (!context.Users.Any(u => u.Email.Equals("rodric.degroote@student.howest.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Degroote",
                    FirstName = "Rodric",
                    Email = "rodric.degroote@student.howest.be",
                    UserName = "rodric.degroote@student.howest.be",
                    Address = "De Patine 47",
                    City = "Zonnebeke",
                    Country = "Belgium",
                    Zipcode = "8980"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, userRole);
            }
        }
    }
}
