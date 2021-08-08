using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketService.Models;

namespace BasketService.Helpers.ExceptionMessages
{
    public static class ExceptionMessages
    {
        public static readonly KeyValue BASKET_NULL_USERNAME = new KeyValue("BASKET_NULL_OR_EMPTY_USERNAME", "Kullanıcı adı dolu olmalıdır.");
        public static readonly KeyValue BASKETITEM_ID_SHOULD_BE_GREATER_THAN_ZERO = new KeyValue("BASKETITEM_ID_SHOULD_BE_GREATER_THAN_ZERO", "Ürün ID'si 0'dan büyük olmalıdır.");
        public static readonly KeyValue QUANTITY_SHOULD_BE_GREATER_THAN_ZERO = new KeyValue("QUANTITY_SHOULD_BE_GREATER_THAN_ZERO", "Ürün sayısı 0'dan büyük olmalıdır.");
        public static readonly KeyValue PRICE_SHOULD_BE_GREATER_THAN_ZERO = new KeyValue("PRICE_SHOULD_BE_GREATER_THAN_ZERO", "Ürün fiyatı 0'dan büyük olmalıdır.");
        public static readonly KeyValue NULL_BASKET_ITEM_TITLE = new KeyValue("NULL_BASKET_ITEM_TITLE", "Ürün başlığı hatalı.");
    }
}
