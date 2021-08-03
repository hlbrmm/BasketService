using System.Collections.Generic;

namespace BasketService.Models
{
    public class Basket
    {
        public string userName { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public BasketInfo BasketInfo { get; set; }
    }
}
