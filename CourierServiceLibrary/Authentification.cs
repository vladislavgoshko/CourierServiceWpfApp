using System;
using CourierServiceLibrary.Models;
using CourierServiceLibrary.Repository;

namespace CourierServiceLibrary
{
    /// <summary>
    /// Метод для работы с профилем пользователя
    /// </summary>
    public class Authentification
    {
        /// <summary>
        /// Пользователь вошедший в систему
        /// </summary>
        public static User signedUser { get; private set; }

        private readonly IRepository<User> _rep;
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="rep"></param>
        public Authentification(IRepository<User> rep)
        {
            _rep = rep;
        }
        /// <summary>
        /// Метод для входа в систему
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Метод для выхода из системы
        /// </summary>
        public void UnSign()
        {
            signedUser = null;
        }
    }
}
