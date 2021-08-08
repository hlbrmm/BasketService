using BasketService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BasketService.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly ILogger _logger;

        public BasketRepository(IDatabase database, ILogger logger)
        {
            _database = database;
            _logger = logger;
        }

        public bool DeleteBasket(string userName)
        {
            return _database.KeyDelete(userName);
        }

        public Basket GetBasket(string userName)
        {
            var customerBasket = _database.StringGet(userName);

            if (customerBasket.HasValue)
            {
                return JsonConvert.DeserializeObject<Basket>(customerBasket);
            }
            {
                _logger.LogInformation("GetBasket Repository returned null. userName : {0}", userName);
                return null;
            }
        }

        public Basket UpdateBasket(Basket basket)
        {
            string serializedBasket = JsonConvert.SerializeObject(basket);
            var success = _database.StringSet(basket.userName, serializedBasket);

            return success ? this.GetBasket(basket.userName) : null;
        }
    }
}
