using BasketService.Exceptions;
using BasketService.Models;
using BasketService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BasketService.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServiceV1 _basketServiceV1;
        private readonly ILogger _logger;

        public BasketController(IBasketServiceV1 basketServiceV1, ILogger logger)
        {
            _basketServiceV1 = basketServiceV1;
            _logger = logger;
        }

        [Route("getbasket")]
        [HttpGet]
        public Basket GetBasket(string userName)
        {
            try
            {
                return _basketServiceV1.GetBasket(userName);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("GetBasket BusinessException : {0},{1} username: {2}", ex.BusinessExceptionMessage.errorCode, ex.BusinessExceptionMessage.errorDetail, userName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetBasket error: " + ex.StackTrace);
                throw;
            }
        }

        [Route("updatebasket")]
        [HttpPost]
        public Basket UpdateBasket(Basket basket)
        {
            try
            {
                return _basketServiceV1.UpdateBasket(basket);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("UpdateBasket BusinessException : {0},{1} basket: {2}", ex.BusinessExceptionMessage.errorCode, ex.BusinessExceptionMessage.errorDetail, basket.ToString());
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateBasket error: " + ex.StackTrace);
                throw;
            }
        }

        [Route("deletebasket")]
        [HttpDelete]
        public bool DeleteBasket(string userName)
        {
            try
            {
                return _basketServiceV1.DeleteBasket(userName);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("DeleteBasket BusinessException : {0},{1} username: {2}", ex.BusinessExceptionMessage.errorCode, ex.BusinessExceptionMessage.errorDetail, userName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteBasket error: " + ex.StackTrace);
                throw;
            }
        }

        [Route("addproducttobasket")]
        [HttpPost]
        public Basket AddProductToBasket(string userName, BasketItem basketItem)
        {
            try
            {
                return _basketServiceV1.AddProductToBasket(userName, basketItem);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("AddProductToBasket BusinessException : {0},{1} basketItem: {2}", ex.BusinessExceptionMessage.errorCode, ex.BusinessExceptionMessage.errorDetail, basketItem.ToString());
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("AddProductToBasket error: " + ex.StackTrace);
                throw;
            }
        }

        [Route("removefrombasket")]
        [HttpPost]
        public Basket RemoveFromBasket(string userName, BasketItem basketItem)
        {
            try
            {
                return _basketServiceV1.RemoveFromBasket(userName, basketItem);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("RemoveFromBasket BusinessException : {0},{1} username: {2}, basketItem : {3}", ex.BusinessExceptionMessage.errorCode, ex.BusinessExceptionMessage.errorDetail, userName, basketItem.ToString());
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("RemoveFromBasket error: " + ex.StackTrace);
                throw;
            }
        }
    }
}
