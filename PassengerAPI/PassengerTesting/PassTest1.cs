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
    public class TestsTicketType
    {
        systemdbContext sysdb;

        List<TicketType> tt;

        [SetUp]
        public void SetupTicketType()
        {
            tt = new List<TicketType>()
            {
                new TicketType {Id=11, Category="Non AC Seating", Price=600},
                new TicketType {Id=12, Category="Non AC Sleeper", Price=700},
                new TicketType {Id=13, Category="AC Seating", Price=800}
            };

            var ticket = tt.AsQueryable();
            var mockSet = new Mock<DbSet<TicketType>>();

            mockSet.As<IQueryable<TicketType>>().Setup(m => m.Provider).Returns(ticket.Provider);
            mockSet.As<IQueryable<TicketType>>().Setup(m => m.Expression).Returns(ticket.Expression);
            mockSet.As<IQueryable<TicketType>>().Setup(m => m.ElementType).Returns(ticket.ElementType);
            mockSet.As<IQueryable<TicketType>>().Setup(m => m.GetEnumerator()).Returns(ticket.GetEnumerator());

            var mockContext = new Mock<systemdbContext>();

            mockContext.Setup(c => c.TicketType).Returns(mockSet.Object);

            sysdb = mockContext.Object;
        }

        [Test]
        public void GetTicketType_ValidInput_OkRequest()
        {
            var mock = new Mock<passenger>(sysdb);

            PassengerController obj = new PassengerController(mock.Object);

            var data = obj.GetTicketType();

            var res = data as ObjectResult;

            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetTicketType_InvalidInput_ReturnsBadRequest()
        {
            try
            {
                var mock = new Mock<passenger>(sysdb);

                PassengerController obj = new PassengerController(mock.Object);

                var data = obj.GetTicketType();

                var res = data as BadRequestResult;

                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetTicketType_ReturnNotNullList()
        {
            var mock = new Mock<passenger>(sysdb);

            PassengerController obj = new PassengerController(mock.Object);

            var data = obj.GetTicketType();

            var res = data as ObjectResult;

            Assert.IsNotNull(data);
        }
    }
}