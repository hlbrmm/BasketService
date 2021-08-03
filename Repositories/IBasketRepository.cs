using BasketService.Models;

namespace BasketService.Repositories
{
    public interface IBasketRepository
    {
        Basket UpdateBasket(Basket basket);
        Basket GetBasket(string userName);
        bool DeleteBasket(string userName);
    }
}
