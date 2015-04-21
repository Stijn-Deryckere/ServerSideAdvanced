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
    public class WebshopContext : DbContext
    {
        public static WebshopContext Create()
        {
            return new WebshopContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<String>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<String>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<OS> OSs { get; set; }
    }
}
