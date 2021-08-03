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

        public BasketRepository(IDatabase database)
        {
            _database = database;
        }
        public bool DeleteBasket(string userName)
        {
            return _database.KeyDelete(userName);
        }

        public Basket GetBasket(string userName)
        {
            var customerBasket = _database.StringGet(userName);

            if (customerBasket.IsNullOrEmpty)
            {
                _logger.LogInformation("GetBasket - Basket does not exist! UserName : {0}" + userName);
                return null;
            }
            else
            {
                _logger.LogInformation("GetBasket - Success! UserName : {0}" + userName);
                return JsonConvert.DeserializeObject<Basket>(customerBasket);
            }
        }

        public Basket UpdateBasket(Basket basket)
        {
            string serializedBasket = JsonConvert.SerializeObject(basket);

            var success = _database.StringSet(basket.userName, serializedBasket);

            if (success)
            {
                _logger.LogInformation("Basket updated successfully!");
                return this.GetBasket(basket.userName);
            }
            else
            {
                _logger.LogInformation("Basket update error!");
                return null;
            }
        }
    }
}
