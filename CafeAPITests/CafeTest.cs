using System;
using NUnit.Framework;
using Newtonsoft.Json;
using CafeAPITests.Models;
using System.Globalization;
using CafeAPI.Services;
    

namespace CafeAPITests
{
   
    [TestFixture]
    public class CafeTests
    {
        private CafeAPIHelper apiHelper;
        [SetUp]
        public void Setup()
        {
            apiHelper = new CafeAPIHelper();
        }

        [Test]
        [TestCase("admin", "admin123")]
        [TestCase("user", "user123")]

        public void LoginTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsTrue(res);
            Assert.IsNotNull(token);
            Assert.AreEqual(errMsg, "");
        }

        [Test]
        [TestCase("admin", "a123")]
        [TestCase("user", "u123")]

        public void LoginFailTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsFalse(res);
            Assert.AreEqual(token, "");
            Assert.IsNotNull(errMsg);
        }


        [Test]
        public void GetCafesTest()
        {
            var cafes = apiHelper.GetCafesAsync().Result;
            Console.WriteLine(JsonConvert.SerializeObject(cafes));
            Assert.IsNotNull(cafes);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void GetCafeTest(int cafeId)
        {
            var cafe = apiHelper.GetCafeAsync(cafeId).Result;
            Assert.IsNotNull(cafe);
            Assert.IsNotNull(cafe.CustomerName);
        }

        [Test]
        public void InsertcafeTest()
        {
            var newCafe= new Cafe();
            newCafe.CuisineId=21;
            newCafe.FoodId=100;
            newCafe.CustomerName = "swathy";
            newCafe.FoodName = "white pasta";
            newCafe.CuisineType = "Italy";
            newCafe.Price = 90;
            newCafe.Status = "placed";
            



            var insCafe = apiHelper.CreateCafeAsync(newCafe).Result;
            Assert.IsNotNull(insCafe);
            Assert.Greater(insCafe.CafeId, 0);

            //Get Consumer and Validate
            //GetConsumerTest(insConsumer.ConsumerId);
            var cafe= apiHelper.GetCafeAsync(insCafe.CafeId).Result;
            Assert.IsNotNull(cafe);
            Assert.IsNotNull(cafe.CustomerName);

            Assert.AreEqual(newCafe.CustomerName, cafe.CustomerName);

            //Updated Consumer
            var updCafe= new Cafe();
            updCafe.CuisineId=21;
            updCafe.FoodId=100;
            updCafe.CustomerName = "swathy";
            updCafe.FoodName = "white pasta";
            updCafe.CuisineType = "Italy";
            updCafe.Price =90;
            updCafe.Status = "placed";
            
            updCafe.CafeId = insCafe.CafeId;

            var updatedCafe = apiHelper.UpdateCafeAsync(updCafe).Result;
            Assert.IsNotNull(updatedCafe);
            Assert.AreEqual(updatedCafe.CustomerName, updatedCafe.CustomerName);

            cafe = apiHelper.GetCafeAsync(insCafe.CafeId).Result;
            Assert.IsNotNull(cafe);

            //Delete consumer
            var delCafeId = apiHelper.DeleteCafeAsync(insCafe.CafeId).Result;
            Assert.AreEqual(insCafe.CafeId, delCafeId);

            cafe = apiHelper.GetCafeAsync(insCafe.CafeId).Result;
            Assert.IsNull(cafe);
        }
    }
}
     
