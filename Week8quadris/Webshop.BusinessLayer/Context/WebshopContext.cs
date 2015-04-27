using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.BusinessLayer.Context
{
    public class WebshopContext : IdentityDbContext<ApplicationUser>
    {
        public static WebshopContext Create()
        {
            return new WebshopContext();
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<OS> OSs { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
