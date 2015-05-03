using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iotshop.BusinessLayer.Context;
using Iotshop.Models;

namespace Iotshop.BusinessLayer.Repositories
{
    public class FormRepository : GenericRepository<Form>, Iotshop.BusinessLayer.Repositories.IFormRepository
    {
        public FormRepository()
        {

        }

        public FormRepository(IotshopContext context)
            :base(context)
        {

        }

        public override Form Insert(Form entity)
        {
            this.context.Entry<FormTopic>(entity.NewFormTopic).State = System.Data.Entity.EntityState.Unchanged;
            this.context.Forms.Add(entity);
            this.SaveChanges();
            return entity;
        }
    }
}
