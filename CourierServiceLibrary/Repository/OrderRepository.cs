using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CourierServiceLibrary.Models;


namespace CourierServiceLibrary.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly string fileName;
        public OrderRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public void Create(Order entity)
        {
            List<Order> ordersInFile = Read().ToList();
            int i = 1;
            while (ordersInFile.Any(t => t.Id == entity.Id))
                entity.Id = i++;
            ordersInFile.Add(entity);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(ordersInFile));
        }

        public IEnumerable<Order> Read()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Orders.json")) ?? new List<Order>();
        }

        public void Delete(int id)
        {
            List<Order> ordersInFile = Read().ToList();

            ordersInFile.RemoveAt(ordersInFile.FindIndex(t => t.Id == id));

            File.WriteAllText(fileName, JsonConvert.SerializeObject(ordersInFile));
        }

    }
}
