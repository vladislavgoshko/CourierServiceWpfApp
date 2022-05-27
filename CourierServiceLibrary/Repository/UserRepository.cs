using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using CourierServiceLibrary.Models;

namespace CourierServiceLibrary.Repository
{
    /// <summary>
    /// Репозиторий для пользователей
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private readonly string fileName;
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="fileName"></param>
        public UserRepository(string fileName)
        {
            this.fileName = fileName;
        }
        /// <summary>
        /// Метод для создания сущности пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="Exception"></exception>
        public void Create(User entity)
        {
            List<User> usersInFile = Read().ToList();
            int i = 1;
            while (usersInFile.Any(t => t.Login == entity.Login))
                throw new Exception("Такой логин уже занят");
            while (usersInFile.Any(t => t.Id == entity.Id)) 
                entity.Id = i++;
            usersInFile.Add(entity);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(usersInFile));
        }
        /// <summary>
        /// Метод для чтения данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Read()
        {
            return JsonConvert.DeserializeObject<IEnumerable<User>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Users.json")) ?? new List<User>();
        }
        /// <summary>
        /// Метод для удаления пользователя
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<User> usersInFile = Read().ToList();

            usersInFile.RemoveAt(usersInFile.FindIndex(t => t.Id == id));

            File.WriteAllText(fileName, JsonConvert.SerializeObject(usersInFile));
        }
        /// <summary>
        /// Метод для изменения данных о пользователе
        /// </summary>
        /// <param name="entity"></param>
        public void Update(User entity)
        {
            List<User> toursInFile = Read().ToList();

            int index = toursInFile.FindIndex(t => t.Id == entity.Id);

            toursInFile[index] = entity;

            File.WriteAllText(fileName, JsonConvert.SerializeObject(toursInFile));
        }
    }
}
