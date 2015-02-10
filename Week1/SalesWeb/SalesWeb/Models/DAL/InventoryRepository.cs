using DBHelper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SalesWeb.Models.DAL
{
    public class InventoryRepository
    {

        //methoden
        public static List<Inventory> GetInventories()
        {
            using (SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories select i);
                return query.ToList<Inventory>();
            }
        }

        public static List<Inventory> FindInventoriesByProductId(int productID)
        {
            using (SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories.Include(i => i.Product) where i.Product.ProductID == productID select i);
                return query.ToList<Inventory>();
            }
        }

        public static List<Inventory> FindInventoriesByLocation(string row, int position)
        {
            using(SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories.Include(i => i.Product) where i.Row ==  row && i.Position == position select i);
                return query.ToList<Inventory>();
            }
        }
    }
}