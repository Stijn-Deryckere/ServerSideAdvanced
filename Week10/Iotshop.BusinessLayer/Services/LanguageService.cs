using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iotshop.BusinessLayer.Repositories;
using Iotshop.Models;

namespace Iotshop.BusinessLayer.Services
{
    public class LanguageService : Iotshop.BusinessLayer.Services.ILanguageService
    {
        private IGenericRepository<AvailableCulture> AvailableCultureRepo = null;

        public LanguageService(IGenericRepository<AvailableCulture> availableCultureRepo)
        {
            this.AvailableCultureRepo = availableCultureRepo;
        }

        public IEnumerable<AvailableCulture> AllAvailableCultures()
        {
            return this.AvailableCultureRepo.All();
        }

        public AvailableCulture AvailableCultureById(int id)
        {
            return this.AvailableCultureRepo.GetByID(id);
        }
    }
}
