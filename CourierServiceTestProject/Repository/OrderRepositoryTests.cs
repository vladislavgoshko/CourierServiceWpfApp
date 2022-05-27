using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

using CourierServiceLibrary.Models;
using System.IO;

namespace CourierServiceLibrary.Repository.Tests
{
    [TestClass()]
    public class OrderRepositoryTests
    {
        Order order = new Order()
        {
            Created = DateTime.Now,
            StartCoords = new double[] { 34.1, 90.0 },
            EndCoords = new double[] { 21, 998.12 },
            Weigth = 345.1,
            Height = 1312,
            Length = 123,
            Width = 4235,
            CourierId = 2,
            Message = "Письмо от администрации",
            UserId = 1,
            Readiness = Readiness.Completed,
            StartedExecuting = DateTime.Now
        };

        [TestMethod()]
        public void CreateTest()
        {
            File.WriteAllText($@"{Directory.GetCurrentDirectory()}\Data\Orders.json", "");
            ProjectResources.Instance.OrdersRepository.Create(order);
            IEnumerable<Order> orders = ProjectResources.Instance.OrdersRepository.Read();
            Assert.AreEqual(orders.Last(), order);
        }

        [TestMethod()]
        public void ReadTest()
        {
            ProjectResources.Instance.OrdersRepository.Create(order);
            IEnumerable<Order> orders = ProjectResources.Instance.OrdersRepository.Read();
            Assert.AreEqual(order, orders.Last());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ProjectResources.Instance.OrdersRepository.Delete(order.Id);
            IEnumerable<Order> orders = ProjectResources.Instance.OrdersRepository.Read();
            Assert.IsFalse(orders.Any());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            order.Message = "";
            order.CourierId = 12;
            order.Readiness = Readiness.Canceled;
            ProjectResources.Instance.OrdersRepository.Update(order);
            IEnumerable<Order> orders = ProjectResources.Instance.OrdersRepository.Read();
            Assert.AreEqual(order, orders.Last());
        }
    }
}