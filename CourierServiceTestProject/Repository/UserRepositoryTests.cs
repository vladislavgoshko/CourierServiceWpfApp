using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourierServiceLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CourierServiceLibrary.Models;

namespace CourierServiceLibrary.Repository.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        User user = new User()
        {
            Name = "admin",
            SecondName = "admin",
            Role = UserRole.Admin,
            Login = "admin",
            Password = "admin",
        };

        [TestMethod()]
        public void CreateTest()
        {
            File.WriteAllText($@"{Directory.GetCurrentDirectory()}\Data\Users.json", "");
            ProjectResources.Instance.UsersRepository.Create(user);
            IEnumerable<User> users = ProjectResources.Instance.UsersRepository.Read();
            Assert.AreEqual(users.Last(), user);
        }

        [TestMethod()]
        public void ReadTest()
        {
            ProjectResources.Instance.UsersRepository.Create(user);
            IEnumerable<User> users = ProjectResources.Instance.UsersRepository.Read();
            Assert.AreEqual(user, users.Last());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ProjectResources.Instance.UsersRepository.Delete(user.Id);
            IEnumerable<User> users = ProjectResources.Instance.UsersRepository.Read();
            Assert.IsFalse(users.Any());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            user.Role = UserRole.Client;
            user.Login = "vladislav";
            user.Password = "strongpassword";
            ProjectResources.Instance.UsersRepository.Update(user);
            IEnumerable<User> users = ProjectResources.Instance.UsersRepository.Read();
            Assert.AreEqual(user, users.Last());
            Assert.AreEqual(user, users.Last());
        }
    }
}