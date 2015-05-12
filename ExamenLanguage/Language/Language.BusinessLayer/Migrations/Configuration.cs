namespace Language.BusinessLayer.Migrations
{
    using Language.BusinessLayer.Context;
    using Language.Models.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Language.BusinessLayer.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {


            //AvailableCulture availableCulture1 = new AvailableCulture()
            //{
            //    Name = "Nederlands",
            //    Code = "nl-BE"
            //};

            //AvailableCulture availableCulture2 = new AvailableCulture()
            //{
            //    Name = "English",
            //    Code = "en-US"
            //};

            //context.AvailableCultures.Add(availableCulture1);
            //context.AvailableCultures.Add(availableCulture2);

            //MakeDevices(context);
            //AddRoles(context);
            //context.SaveChanges();
        }

        //private void MakeDevices(ApplicationDbContext context)
        //{
        //    Device device1 = new Device()
        //    {
        //        Name = "Raspberry Pi Model 2B",
        //        Description = "De Raspberry Pi 2 Model B is de laatste telg in de Raspberry Pi familie. Hij beschikt over een BCM2836 ARMv7 Quad Core Processor op 900MHz en 1GB RAM. Hiermee is de Raspberry Pi 2 Model B 6x sneller dan zijn voorgangers en heeft hij twee keer zoveel geheugen.",
        //        Price = 39.95
        //    };

        //    Device device2 = new Device()
        //    {
        //        Name = "Raspberry Pi Model A+",
        //        Description = "De Raspberry Pi Model A+ is een uitgeklede versie van de Raspberry Pi Model B+ zonder Ethernet, één USB poort en 256MB RAM. Het weglaten van bepaalde features in de Raspberry Pi Model A+ heeft als resultaat dat deze $10 goedkoper is dan de Model B+ en bijna een derde minder energie gebruikt.",
        //        Price = 24.95
        //    };

        //    context.Devices.Add(device1);
        //    context.Devices.Add(device2);
        //}

        //private void AddRoles(ApplicationDbContext context)
        //{
        //    String adminRole = "Administrator";
        //    String userRole = "User";
        //    IdentityResult roleResult;
        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    if (!roleManager.RoleExists(adminRole))
        //        roleResult = roleManager.Create(new IdentityRole(adminRole));
        //    if (!roleManager.RoleExists(userRole))
        //        roleResult = roleManager.Create(new IdentityRole(userRole));
        //    if (!context.Users.Any(u => u.Email.Equals("dieter.de.preeter@howest.be")))
        //    {
        //        var store = new UserStore<ApplicationUser>(context);
        //        var manager = new UserManager<ApplicationUser>(store);
        //        var user = new ApplicationUser()
        //        {
        //            Name = "De Preester",
        //            Firstname = "Dieter",
        //            Email = "dieter.de.preester@howest.be",
        //            UserName = "dieter.de.preester@howest.be",
        //            Address = "Graaf Karel De Goedelaan 1",
        //            City = "Kortrijk",
        //            Country = "Belgium",
        //            Zipcode = "8500"
        //        };
        //        manager.Create(user, "-Password1");
        //        manager.AddToRole(user.Id, adminRole);
        //    }
        //    if (!context.Users.Any(u => u.Email.Equals("kristof@kristofcolpaert.com")))
        //    {
        //        var store = new UserStore<ApplicationUser>(context);
        //        var manager = new UserManager<ApplicationUser>(store);
        //        var user = new ApplicationUser()
        //        {
        //            Name = "Colpaert",
        //            Firstname = "Kristof",
        //            Email = "kristof@kristofcolpaert.com",
        //            UserName = "kristof@kristofcolpaert.com",
        //            Address = "Zwalmkouter 12",
        //            City = "Erembodegem",
        //            Country = "Belgium",
        //            Zipcode = "9320"
        //        };
        //        manager.Create(user, "-Password1");
        //        manager.AddToRole(user.Id, adminRole);
        //    }
        //    if (!context.Users.Any(u => u.Email.Equals("rodric.degroote@student.howest.be")))
        //    {
        //        var store = new UserStore<ApplicationUser>(context);
        //        var manager = new UserManager<ApplicationUser>(store);
        //        var user = new ApplicationUser()
        //        {
        //            Name = "Degroote",
        //            Firstname = "Rodric",
        //            Email = "rodric.degroote@student.howest.be",
        //            UserName = "rodric.degroote@student.howest.be",
        //            Address = "De Patine 47",
        //            City = "Zonnebeke",
        //            Country = "Belgium",
        //            Zipcode = "8980"
        //        };
        //        manager.Create(user, "-Password1");
        //        manager.AddToRole(user.Id, userRole);
        //    }
        //}
    }
}
