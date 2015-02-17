using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models.DAL
{
    public class OperatingSystemRepository
    {
        public static List<OS> Get()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {

            }
        }
    }
}