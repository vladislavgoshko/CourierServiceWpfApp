using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models
{
    /// <summary>
    /// Class of User
    /// </summary>
    public class User : IBaseEntity
    {
        /// <summary>
        /// User's role
        /// </summary>
        public UserRole Role { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Second name
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }/*
        public User(UserRole role, int id, string name, string secondName, string login, string password)
        {
            Role = role;
            Id = id;
            Name = name;
            SecondName = secondName;
            Login = login;
            Password = password;
        }*/
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, SecondName: {SecondName}, Login: {Login}, Role: {Role}";
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User u = (User)obj;
                return Id == u.Id && Name == u.Name && SecondName == u.SecondName && 
                    Role == u.Role && Login == u.Login && Password == u.Password;
            }
        }
        public override int GetHashCode()
        {
            return Role.GetHashCode() * Id.GetHashCode() * Name.GetHashCode() * Name.GetHashCode() * 
                SecondName.GetHashCode() * Login.GetHashCode() * Password.GetHashCode();
        }
    }
}
