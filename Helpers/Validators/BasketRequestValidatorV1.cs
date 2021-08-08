using BasketService.Exceptions;
using BasketService.Models;
using System;

namespace BasketService.Helpers.Validators
{
    public class BasketRequestValidatorV1
    {
        public void ValidateUserName(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new BusinessException(ExceptionMessages.ExceptionMessages.BASKET_NULL_USERNAME.Key, ExceptionMessages.ExceptionMessages.BASKET_NULL_USERNAME.Value);
            }
        }
        public void ValidateBasketItem(BasketItem basketItem)
        {
            if (basketItem.Id < 0)
            {
                throw new BusinessException(ExceptionMessages.ExceptionMessages.BASKETITEM_ID_SHOULD_BE_GREATER_THAN_ZERO.Key, ExceptionMessages.ExceptionMessages.BASKETITEM_ID_SHOULD_BE_GREATER_THAN_ZERO.Value);
            }
            else if (basketItem.Quantity < 1)
            {
                throw new BusinessException(ExceptionMessages.ExceptionMessages.QUANTITY_SHOULD_BE_GREATER_THAN_ZERO.Key, ExceptionMessages.ExceptionMessages.QUANTITY_SHOULD_BE_GREATER_THAN_ZERO.Value);
            }
            else if (basketItem.Price < 0)
            {
                throw new BusinessException(ExceptionMessages.ExceptionMessages.PRICE_SHOULD_BE_GREATER_THAN_ZERO.Key, ExceptionMessages.ExceptionMessages.PRICE_SHOULD_BE_GREATER_THAN_ZERO.Value);
            }
            else if (String.IsNullOrEmpty(basketItem.Title))
            {
                throw new BusinessException(ExceptionMessages.ExceptionMessages.NULL_BASKET_ITEM_TITLE.Key, ExceptionMessages.ExceptionMessages.NULL_BASKET_ITEM_TITLE.Value);
            }
        }

        public void ValidateBasket(Basket basket)
        {
            this.ValidateUserName(basket.userName);
            for (int i = 0; i < basket.BasketItems.Count; i++)
            {
                this.ValidateBasketItem(basket.BasketItems[i]);
            }
        }
    }
}
