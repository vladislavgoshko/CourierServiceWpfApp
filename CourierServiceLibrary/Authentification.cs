using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using CourierServiceLibrary.Models;
using CourierServiceLibrary.Repository;

namespace CourierServiceLibrary
{
    public class Authentification
    {
        public static User signedUser { get; private set; }

        private readonly IRepository<User> _rep;
        public Authentification(IRepository<User> rep)
        {
            _rep = rep;
        }

        public void SignIn(string login, string password)
        {
            foreach (User user in _rep.Read())
            {
                if (user.Login == login && user.Password == password)
                {
                    signedUser = user;
                }

            }
            if (signedUser == null)
                throw new Exception("Неверный логин или пароль");
        }
        public void UnSign()
        {
            signedUser = null;
        }
    }
}
