using AuthenticationAPI.Controllers;
using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuthenticationTesting
{
    public class Tests
    {
        systemdbContext sysdb;

        iauthentication iauth;

        List<PassengerAcc> user = new List<PassengerAcc>();
        IQueryable<PassengerAcc> userdata;
        Mock<DbSet<PassengerAcc>> mockSet;
        Mock<systemdbContext> usercontextmock;

        [SetUp]
        public void Setup1()
        {
            var user = new List<PassengerAcc>()
            {
                new PassengerAcc{Id=21, PassengerId=2221, PassengerPassword="passenger@21", PassengerName="Keshav", PassengerAddress="Lane21", PassengerMobile="8872892242"},
                new PassengerAcc{Id=22, PassengerId=2222, PassengerPassword="passenger@22", PassengerName="Praveen", PassengerAddress="Lane22", PassengerMobile="7828298224"},
                new PassengerAcc{Id=23, PassengerId=2223, PassengerPassword="passenger@23", PassengerName="Rohit", PassengerAddress="Lane23", PassengerMobile="9872542242"}
            };
            
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<PassengerAcc>>();
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<systemdbContext>();
            usercontextmock = new Mock<systemdbContext>(p);
            usercontextmock.Setup(x => x.PassengerAcc).Returns(mockSet.Object);
        }
        
        [Test]
        public void LoginTest1()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            
            var controller = new TokenValidateController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new User { PassengerId = "2221", PassengerPassword = "passenger@21" }) as OkObjectResult;

            Assert.AreEqual(200, auth.StatusCode);
        }

        [Test]
        public void LoginTestFail()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            
            var controller = new TokenValidateController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new User { PassengerId = "854", PassengerPassword = "c123" }) as OkObjectResult;
            
            Assert.IsNull(auth);

        }
    }
}