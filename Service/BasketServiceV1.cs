using BasketService.Exceptions;
using BasketService.Helpers.ExceptionMessages;
using BasketService.Helpers.Validators;
using BasketService.Models;
using BasketService.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BasketService.Service
{
    public class BasketServiceV1 : IBasketServiceV1
    {
        BasketRequestValidatorV1 requestValidatorV1 = new BasketRequestValidatorV1();

        private readonly IBasketRepository _basketRepository;
        public BasketServiceV1(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public Basket GetBasket(string username)
        {
            requestValidatorV1.ValidateUserName(username);

            Basket userBasket = _basketRepository.GetBasket(username);

            if (userBasket != null)
            {
                CalculateBasketInfo(userBasket);
                return userBasket;
            }
            else
            {
                return null;
            }
        }

        public Basket UpdateBasket(Basket basket)
        {
            requestValidatorV1.ValidateBasket(basket);

            return _basketRepository.UpdateBasket(basket);
        }
        public Basket AddProductToBasket(string userName, BasketItem basketItem)
        {
            requestValidatorV1.ValidateUserName(userName);
            requestValidatorV1.ValidateBasketItem(basketItem);

            Basket userBasket = _basketRepository.GetBasket(userName);

            if (userBasket == null)
            {
                userBasket = new Basket();
                userBasket.BasketItems = new List<BasketItem>();
                userBasket.BasketInfo = new BasketInfo();
                userBasket.userName = userName;
                userBasket.BasketItems.Add(basketItem);
            }
            else
            {
                if (userBasket.BasketItems != null && userBasket.BasketItems.Count > 0)
                {
                    var basketItemInuserBasket = userBasket.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefault();

                    if (basketItemInuserBasket != null)
                    {
                        basketItemInuserBasket.Quantity += basketItem.Quantity;
                    }
                }
                else
                {
                    userBasket.BasketItems.Add(basketItem);
                }
            }

            userBasket = CalculateBasketInfo(userBasket);

            return _basketRepository.UpdateBasket(userBasket);
        }

        public bool DeleteBasket(string userName)
        {
            requestValidatorV1.ValidateUserName(userName);

            return _basketRepository.DeleteBasket(userName);
        }


        public Basket RemoveFromBasket(string userName, BasketItem basketItem)
        {
            Basket userBasket = this.GetBasket(userName);

            if (userBasket.BasketItems != null && userBasket.BasketItems.Count > 0)
            {
                var basketItemInUserBasket = userBasket.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefault();

                if (basketItemInUserBasket != null)
                {
                    basketItemInUserBasket.Quantity -= basketItem.Quantity;
                }
                if (basketItemInUserBasket.Quantity <= 0)
                {
                    userBasket.BasketItems.Remove(basketItemInUserBasket);
                }
            }
            else
            {
                throw new BusinessException(ExceptionMessages.BASKETITEM_DOES_NOT_EXIST_IN_BASKET.Key, ExceptionMessages.BASKETITEM_DOES_NOT_EXIST_IN_BASKET.Value);
            }

            userBasket = CalculateBasketInfo(userBasket);

            return _basketRepository.UpdateBasket(CalculateBasketInfo(userBasket));
        }

        public static Basket CalculateBasketInfo(Basket basket)
        {
            if (basket != null)
            {
                basket.BasketInfo.SubTotal = 0;
                basket.BasketInfo.Total = 0;

                if (basket.BasketItems.Count > 0)
                {
                    for (int i = 0; i < basket.BasketItems.Count; i++)
                    {
                        basket.BasketInfo.SubTotal += (basket.BasketItems[i].Quantity * basket.BasketItems[i].Price);
                    }

                    basket.BasketInfo = CalculateTotalAndShipmentFee(basket.BasketInfo);
                }
            }

            return basket;
        }

        public static BasketInfo CalculateTotalAndShipmentFee(BasketInfo basketInfo)
        {
            decimal shipmentFee = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("SHIPMENT_FEE"));
            decimal freeShipmentLimit = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("FREE_SHIPMENT_LIMIT"));

            if (basketInfo.SubTotal > freeShipmentLimit)
            {
                basketInfo.Shipping = 0;
                basketInfo.Total = basketInfo.SubTotal;
            }
            else
            {
                basketInfo.Shipping = shipmentFee;
                basketInfo.Total = basketInfo.SubTotal + shipmentFee;
            }

            return basketInfo;
        }
    }
}
