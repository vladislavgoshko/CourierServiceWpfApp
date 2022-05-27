using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CourierServiceLibrary.Models;
using CourierServiceLibrary;

namespace CourierServiceTestProject.Models.Tests
{
    /// <summary>
    /// Сводное описание для UnitTest1
    /// </summary>
    [TestClass]
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
            StartCoords = new double[] { 24, 90.0 },
            EndCoords = new double[] { 21, 5.12 },
            Weigth = 345.1,
            Height = 1312,
            Length = 123,
            Width = 4235,
            CourierId = 1,
            Message = "Письмо от администрации",
            UserId = 1,
            Readiness = Readiness.Pending,
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
            Readiness = Readiness.Pending,
        };

        [TestMethod()]
        public void AddOrderTest()
        {
            Assert.IsTrue(courier.AddOrder(out string _, 0, 24, order1));
            Assert.IsFalse(courier.AddOrder(out string message, 7, 15, order2));
            Assert.AreEqual(message, "Заказ невозможно выполнить за целый рабочий день");
        }

        [TestMethod()]
        public void GetDistanceFromEndLocationTest()
        {
            Assert.AreEqual(Math.Round(courier.GetDistanceFromEndLocation(new double[] {0, 0}), 2), 22.09);
        }

        [TestMethod()]
        public void IsFreeAndWhenTest()
        {
            Assert.IsFalse(courier.IsFreeAndWhen(out DateTime _));
        }
        [TestMethod()]
        public void TypeToStringTest()
        {
            Assert.AreEqual("Машина", Courier.TypeToString(TransportType.Car));
        }
    }
}
