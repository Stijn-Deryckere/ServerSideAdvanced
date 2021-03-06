﻿using System;
namespace Webshop.BusinessLayer.Repositories
{
    public interface IBasketItemRepository
    {
        System.Collections.Generic.IEnumerable<Webshop.Models.BasketItem> All();
        void Delete(object id);
        Webshop.Models.BasketItem GetByID(object id);
        System.Collections.Generic.IEnumerable<Webshop.Models.BasketItem> GetByUser(Webshop.Models.ApplicationUser user);
        System.Collections.Generic.IEnumerable<Webshop.Models.BasketItem> GetByVisitorGUID(string visitorGUID);
        Webshop.Models.BasketItem Insert(Webshop.Models.BasketItem entity);
        void Update(Webshop.Models.BasketItem entityToUpdate);
        void UpdateUser(Webshop.Models.BasketItem basketItem);
    }
}
