using BasketService.Models;
using BasketService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [Route("GetBasket")]
        [HttpGet]
        public Basket GetBasket(string userName)
        {
            return _basketRepository.GetBasket(userName);
        }

        [Route("UpdateBasket")]
        [HttpPost]
        public Basket UpdateBasket(Basket basket)
        {
            return _basketRepository.UpdateBasket(basket);
        }

        [Route("DeleteBasket")]
        [HttpDelete]
        public bool DeleteBasket(string userName)
        {
            return _basketRepository.DeleteBasket(userName);
        }
    }
}
