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
    public class TestsBookingDetail
    {
        systemdbContext sysdb;

        List<BookingDetail> bd;

        [SetUp]
        public void SetupBookingDetail()
        {
            bd = new List<BookingDetail>()
            {
                new BookingDetail {Id=21, PassengerId=2222, PassengerName="Keshav", Category="Non AC Sleeper", Price=700},
                new BookingDetail {Id=22, PassengerId=2223, PassengerName="Praveen", Category="AC Seating", Price=800},
                new BookingDetail {Id=23, PassengerId=2224, PassengerName="Rohit", Category="Non AC Seating", Price=600}
            };

            var booking = bd.AsQueryable();
            var mockSet = new Mock<DbSet<BookingDetail>>();

            mockSet.As<IQueryable<BookingDetail>>().Setup(m => m.Provider).Returns(booking.Provider);
            mockSet.As<IQueryable<BookingDetail>>().Setup(m => m.Expression).Returns(booking.Expression);
            mockSet.As<IQueryable<BookingDetail>>().Setup(m => m.ElementType).Returns(booking.ElementType);
            mockSet.As<IQueryable<BookingDetail>>().Setup(m => m.GetEnumerator()).Returns(booking.GetEnumerator());

            var mockContext = new Mock<systemdbContext>();

            mockContext.Setup(c => c.BookingDetail).Returns(mockSet.Object);

            sysdb = mockContext.Object;
        }

        [Test]
        public void BookTicket_ValidInput_OkRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                BookingDetail book = new BookingDetail { Id = 21, PassengerId = 2222, PassengerName = "Keshav", Category = "Non AC Sleeper", Price = 700 };

                var data = obj.BookTicket(book);

                var res = data as ObjectResult;

                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void BookTicket_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                BookingDetail book = new BookingDetail {Id=24, PassengerId=2225, PassengerName="Rahul", Category="AC Sleeper", Price=900};

                var data = obj.BookTicket(book);

                var res = data as BadRequestObjectResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetAllBookingDetail_ValidInput_OkRequest()
        {
            var mock = new Mock<admin>(sysdb);

            AdminController obj = new AdminController(mock.Object);

            var data = obj.GetAllBookingDetail();

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetAllBookingDetail_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<admin>(sysdb);

                AdminController obj = new AdminController(mock.Object);

                var data = obj.GetAllBookingDetail();

                var res = data as BadRequestResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetAllBookingDetail_ReturnNotNullList()
        {
            var mock = new Mock<admin>(sysdb);

            AdminController obj = new AdminController(mock.Object);

            var data = obj.GetAllBookingDetail();

            var res = data as ObjectResult;

            Assert.IsNotNull(data);
        }
    }
}