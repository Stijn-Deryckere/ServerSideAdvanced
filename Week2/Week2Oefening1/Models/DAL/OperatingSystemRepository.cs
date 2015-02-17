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
                var query = (from o in context.OperatingSystems select o);
                return query.ToList<OS>();
            }
        }

        public static OS Get(int id)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from o in context.OperatingSystems where o.Id == id select o);
                return query.SingleOrDefault<OS>();
            }
        }
    }
}