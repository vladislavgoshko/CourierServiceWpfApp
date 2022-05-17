using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using CourierServiceLibrary.Models;

namespace CourierServiceLibrary.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly string fileName;
        public UserRepository(string fileName)
        {
            this.fileName = fileName;
        }

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

        public IEnumerable<User> Read()
        {
            return JsonConvert.DeserializeObject<IEnumerable<User>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Users.json")) ?? new List<User>();
        }
        public void Delete(int id)
        {
            List<User> usersInFile = Read().ToList();

            usersInFile.RemoveAt(usersInFile.FindIndex(t => t.Id == id));

            File.WriteAllText(fileName, JsonConvert.SerializeObject(usersInFile));
        }
    }
}
