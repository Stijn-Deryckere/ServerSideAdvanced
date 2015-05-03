using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Context;
using Webshop.Models;

namespace Webshop.BusinessLayer.Repositories
{
    public class FormRepository : GenericRepository<Form>, Webshop.BusinessLayer.Repositories.IFormRepository
    {
        public FormRepository()
        {

        }

        public FormRepository(WebshopContext context)
            :base(context)
        {

        }

        public override Form Insert(Form entity)
        {
            this.context.Entry<FormTopic>(entity.NewFormTopic).State = System.Data.Entity.EntityState.Unchanged;
            this.context.Forms.Add(entity);
            return entity;
        }
    }
}
