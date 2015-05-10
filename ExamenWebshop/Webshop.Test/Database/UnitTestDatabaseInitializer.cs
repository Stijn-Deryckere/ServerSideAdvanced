using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;

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
            //Code om database te vullen met testdata.
        }
    }
}
