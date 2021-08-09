using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BasketService.Controllers;
using BasketService.Models;
using BasketService.Service;
using NUnit.Framework;

namespace BasketService
{
    public class BasketTests
    {
        private readonly IBasketServiceV1 _basketServiceV1;

        public BasketTests(IBasketServiceV1 basketServiceV1)
        {
            _basketServiceV1 = basketServiceV1;

        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_To_Basket_()
        {
            //Arrange
            string userName = "testuser";
            BasketItem macbook = new BasketItem()
            {
                Id = 1,
                Image = "img",
                Price = 10000,
                Quantity = 1,
                Title = "MacBook13"
            };

            Basket resultBasket = new Basket();

            //Act
            resultBasket = _basketServiceV1.AddProductToBasket(userName, macbook);

            //Assert
            Assert.AreEqual(macbook.Id, resultBasket.BasketItems[0].Id);
        }

        [Test]
        public void Add_To_Basket_2_Times_Increas_Quantity()
        {
            //Arrange
            string userName = "testuser";
            BasketItem macbook = new BasketItem()
            {
                Id = 1,
                Image = "img",
                Price = 10000,
                Quantity = 1,
                Title = "MacBook13"
            };

            Basket resultBasket = new Basket();

            //Act
            resultBasket = _basketServiceV1.AddProductToBasket(userName, macbook);

            //Assert
            Assert.AreEqual(macbook.Quantity + 1, resultBasket.BasketItems[0].Quantity);
        }

        [Test]
        public void Remove_From_Basket_Decrease_Quantity_For_More_Than_One_Elements()
        {
            //Arrange
            string userName = "testuser";
            BasketItem macbook = new BasketItem()
            {
                Id = 1,
                Image = "img",
                Price = 10000,
                Quantity = 1,
                Title = "MacBook13"
            };


            //Act
            Basket basketBeforeRemoving = _basketServiceV1.GetBasket(userName);
            Basket resultBasket = _basketServiceV1.RemoveFromBasket(userName, macbook);

            //Assert
            Assert.AreEqual(basketBeforeRemoving.BasketItems[0].Quantity, resultBasket.BasketItems[0].Quantity + 1);
        }

        [Test]
        public void Shipping_Is_Free_Is_Product_Over_FREE_SHIPMENT_LIMIT()
        {
            //Arrange
            string userName = "testuser2";
            BasketItem macbook = new BasketItem()
            {
                Id = 2,
                Image = "img",
                Price = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("FREE_SHIPMENT_LIMIT")) + 1,
                Quantity = 1,
                Title = "MacBook15"
            };

            //Act
            Basket userBasket = _basketServiceV1.AddProductToBasket(userName, macbook);

            //Assert
            Assert.AreEqual(userBasket.BasketInfo.SubTotal, userBasket.BasketInfo.Total);
        }

        [Test]
        public void Shipping_Is_Incuded_When_Product_Under_FREE_SHIPMENT_LIMIT()
        {
            //Arrange
            string userName = "testuser3";
            BasketItem macbook = new BasketItem()
            {
                Id = 2,
                Image = "img",
                Price = Convert.ToDecimal(ConfigurationManager.AppSettings.Get("FREE_SHIPMENT_LIMIT")) - 1,

                Quantity = 1,
                Title = "MacBook15"
            };

            //Act
            Basket userBasket = _basketServiceV1.AddProductToBasket(userName, macbook);

            //Assert
            Assert.AreEqual(userBasket.BasketInfo.SubTotal + Convert.ToDecimal(ConfigurationManager.AppSettings.Get("SHIPMENT_FEE")), userBasket.BasketInfo.Total);
        }
    }
}