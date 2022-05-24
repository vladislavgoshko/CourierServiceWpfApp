using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourierServiceLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierServiceLibrary.Models;
using System.IO;

namespace CourierServiceLibrary.Repository.Tests
{
    [TestClass()]
    public class CourierRepositoryTests
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
        [TestMethod()]
        public void CreateTest()
        {
            File.WriteAllText($@"{Directory.GetCurrentDirectory()}\Data\Couriers.json", "");
            ProjectResources.Instance.CouriersRepository.Create(courier);
            IEnumerable<Courier> couriers = ProjectResources.Instance.CouriersRepository.Read();
            Assert.AreEqual(courier, couriers.Last());
        }

        [TestMethod()]
        public void ReadTest()
        {
            ProjectResources.Instance.CouriersRepository.Create(courier);
            IEnumerable<Courier> couriers = ProjectResources.Instance.CouriersRepository.Read();
            Assert.AreEqual(courier, couriers.Last());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ProjectResources.Instance.CouriersRepository.Delete(courier.Id);
            IEnumerable<Courier> couriers = ProjectResources.Instance.CouriersRepository.Read();
            Assert.IsFalse(couriers.Any());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            courier.SecondName = "Сергеев";
            ProjectResources.Instance.CouriersRepository.Update(courier);
            IEnumerable<Courier> couriers = ProjectResources.Instance.CouriersRepository.Read();
            Assert.AreEqual(courier, couriers.Last());
        }
    }
}