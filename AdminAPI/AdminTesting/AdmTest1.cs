using AdminAPI.Controllers;
using AdminAPI.Models;
using AdminAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminTesting
{
    public class TestsPassengerAcc
    {
        systemdbContext sysdb;

        List<PassengerAcc> pa;

        [SetUp]
        public void SetupPassengerAcc()
        {
            pa = new List<PassengerAcc>()
            {
                new PassengerAcc {Id=21, PassengerId=2222, PassengerPassword="passenger@21", PassengerName="Keshav", PassengerAddress="Lane21", PassengerMobile="8872892242"},
                new PassengerAcc {Id=22, PassengerId=2223, PassengerPassword="passenger@22", PassengerName="Praveen", PassengerAddress="Lane22", PassengerMobile="7828298224"},
                new PassengerAcc {Id=23, PassengerId=2224, PassengerPassword="passenger@23", PassengerName="Rohit", PassengerAddress="Lane23", PassengerMobile="9872542242"}
            };

            var passenger = pa.AsQueryable();
            var mockSet = new Mock<DbSet<PassengerAcc>>();

            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.Provider).Returns(passenger.Provider);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.Expression).Returns(passenger.Expression);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.ElementType).Returns(passenger.ElementType);
            mockSet.As<IQueryable<PassengerAcc>>().Setup(m => m.GetEnumerator()).Returns(passenger.GetEnumerator());

            var mockContext = new Mock<systemdbContext>();

            mockContext.Setup(c => c.PassengerAcc).Returns(mockSet.Object);

            sysdb = mockContext.Object;
        }

        [Test]
        public void RegisterNewPassenger_ValidInput_OkRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                PassengerAcc pass = new PassengerAcc {Id=21, PassengerId=2222, PassengerPassword="passenger@21", PassengerName="Keshav", PassengerAddress="Lane21", PassengerMobile="8872892242"};

                var data = obj.RegisterNewPassenger(pass);

                var res = data as ObjectResult;

                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void RegisterNewPassenger_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                PassengerAcc pass = new PassengerAcc {Id=24, PassengerId=2225, PassengerPassword="passenger@24", PassengerName="Rahul", PassengerAddress="Lane24", PassengerMobile="7737425842"};

                var data = obj.RegisterNewPassenger(pass);

                var res = data as BadRequestObjectResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetAllPassengerDetail_ValidInput_OkRequest()
        {
            var mock = new Mock<admin>(sysdb);

            AdminController obj = new AdminController(mock.Object);

            var data = obj.GetAllPassengerDetail();

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetAllPassengerDetail_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                var data = obj.GetAllPassengerDetail();

                var res = data as BadRequestResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetAllPassengerDetail_ReturnNotNullList()
        {
            var mock = new Mock<admin>(sysdb);

            AdminController obj = new AdminController(mock.Object);

            var data = obj.GetAllPassengerDetail();

            var res = data as ObjectResult;

            Assert.IsNotNull(data);
        }
    }
}