using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BusinessLayer.Repositories;
using Webshop.Models;

namespace Webshop.BusinessLayer.Services
{
    public class BasketItemService : Webshop.BusinessLayer.Services.IBasketItemService
    {
        private IBasketItemRepository BasketItemRepo = null;

        public BasketItemService(IBasketItemRepository basketItemRepository)
        {
            this.BasketItemRepo = basketItemRepository;
        }

        public IEnumerable<BasketItem> AllBasketItems()
        {
            return BasketItemRepo.All();
        }

        public BasketItem BasketItemById(int id)
        {
            return BasketItemRepo.GetByID(id);
        }

        public IEnumerable<BasketItem> BasketItemsByUser(ApplicationUser user)
        {
            return BasketItemRepo.GetByUser(user);
        }

        public IEnumerable<BasketItem> BasketItemsByVisitorGUID(String visitorGUID)
        {
            return BasketItemRepo.GetByVisitorGUID(visitorGUID);
        }

        public BasketItem AddBasketItem(BasketItem basketItem)
        {
            return BasketItemRepo.Insert(basketItem);
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            BasketItemRepo.Update(basketItem);
        }

        public void UpdateBasketItemUser(BasketItem basketItem)
        {
            BasketItemRepo.UpdateUser(basketItem);
        }

        public void DeleteBasketItem(int id)
        {
            BasketItemRepo.Delete(id);
        }
    }
}
