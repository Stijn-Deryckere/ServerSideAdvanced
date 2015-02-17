using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week2Oefening1.Models.DAL
{
    public class FrameworkRepository
    {
        public static List<Framework> Get()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from f in context.Frameworks select f);
                return query.ToList<Framework>();
            }
        }

        public static Framework Get(int id)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from f in context.Frameworks where f.Id == id select f);
                return query.SingleOrDefault<Framework>();
            }
        }
    }
}