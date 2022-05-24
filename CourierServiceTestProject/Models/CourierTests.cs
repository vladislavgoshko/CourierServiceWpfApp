using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourierServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models.Tests
{
    [TestClass()]
    public class CourierTests
    {
        Courier courier = new Courier()
        {
            Name = "Олег",
            SecondName = "Иванов",
            TransportType = TransportType.Car,
            ListOrdersId = new List<int> { 1, 2 },
            Address = new double[] { 23, 45 },
            LastLocation = new double[] { 10, 12 }
        };
        Order order1 = new Order()
        {
            Created = DateTime.Now,
            StartCoords = new double[] { 34.1, 90.0 },
            EndCoords = new double[] { 21, 998.12 },
            Weigth = 345.1,
            Height = 1312,
            Length = 123,
            Width = 4235,
            CourierId = 1,
            Message = "Письмо от администрации",
            UserId = 1,
            Readiness = Readiness.Completed,
        };
        Order order2 = new Order()
        {
            Created = DateTime.Now,
            StartCoords = new double[] { 34.1, 90.0 },
            EndCoords = new double[] { 21, 998.12 },
            Weigth = 345.1,
            Height = 1312,
            Length = 123,
            Width = 4235,
            CourierId = 1,
            Message = "Письмо от администрации",
            UserId = 1,
            Readiness = Readiness.Completed,
        };

        [TestMethod()]
        public void GetDistanceFromEndLocationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IsFreeAndWhenTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TypeToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }
    }
}