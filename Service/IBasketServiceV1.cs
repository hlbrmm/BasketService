using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketService.Models;

namespace BasketService.Service
{
    public interface IBasketServiceV1
    {
        Basket GetBasket(string username);
        Basket UpdateBasket(Basket basket);
        bool DeleteBasket(string userName);
        Basket AddProductToBasket(string userName, BasketItem basketItem);
        Basket RemoveFromBasket(string userName, BasketItem basketItem);
    }
}
