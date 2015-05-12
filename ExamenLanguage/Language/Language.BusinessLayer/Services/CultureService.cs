using Language.BusinessLayer.Repositories;
using Language.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.BusinessLayer.Services
{
    public class CultureService : Language.BusinessLayer.Services.ICultureService
    {
        private IGenericRepository<AvailableCulture> CultureRepo = null;

        public CultureService(IGenericRepository<AvailableCulture> cultureRepo)
        {
            this.CultureRepo = cultureRepo;
        }

        public IEnumerable<AvailableCulture> AllCultures()
        {
            return this.CultureRepo.All();
        }

        public AvailableCulture CultureById(int id)
        {
            return this.CultureRepo.GetByID(id);
        }
    }
}
