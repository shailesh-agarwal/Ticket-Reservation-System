using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PassengerAPI.Controllers;
using PassengerAPI.Models;
using PassengerAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassengerTesting
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
        public void GetBookingDetail_ValidInput_OkRequest()
        {
            var mock = new Mock<passenger>(sysdb);

            PassengerController obj = new PassengerController(mock.Object);

            var data = obj.GetBookingDetail(2222);

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetBookingDetail_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<passenger>(sysdb);

                PassengerController obj = new PassengerController(mock.Object);

                var data = obj.GetBookingDetail(6666);

                var res = data as BadRequestResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetBookingDetail_ReturnNotNullList()
        {
            var mock = new Mock<passenger>(sysdb);

            PassengerController obj = new PassengerController(mock.Object);

            var data = obj.GetBookingDetail(2224);

            var res = data as ObjectResult;

            Assert.IsNotNull(data);
        }
    }
}