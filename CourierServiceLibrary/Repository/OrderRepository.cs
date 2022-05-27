using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using CourierServiceLibrary.Models;


namespace CourierServiceLibrary.Repository
{
    /// <summary>
    /// Репозиторий для заказов
    /// </summary>
    public class OrderRepository : IRepository<Order>
    {
        private readonly string fileName;
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="fileName"></param>
        public OrderRepository(string fileName)
        {
            this.fileName = fileName;
        }
        /// <summary>
        /// Метод для создания сущности заказа
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Order entity)
        {
            List<Order> ordersInFile = Read().ToList();
            int i = 1;
            while (ordersInFile.Any(t => t.Id == entity.Id))
                entity.Id = i++;
            ordersInFile.Add(entity);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(ordersInFile));
        }
        /// <summary>
        /// Метод для чтения данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> Read()
        {
            IEnumerable<Order> orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Orders.json")) ?? new List<Order>();
            bool isChanged = false;
            foreach (Order entity in orders)
                if (entity.StartedExecuting.Date < DateTime.Now.Date && entity.StartedExecuting.Date > new DateTime(2022,1,1))
                {
                    isChanged = true;
                    entity.Readiness = Readiness.Completed;
                }
            if (isChanged)
                File.WriteAllText(fileName, JsonConvert.SerializeObject(orders));
            return orders;
        }
        /// <summary>
        /// Метод для удаления заказа
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<Order> ordersInFile = Read().ToList();

            ordersInFile.RemoveAt(ordersInFile.FindIndex(t => t.Id == id));

            File.WriteAllText(fileName, JsonConvert.SerializeObject(ordersInFile));
        }
        /// <summary>
        /// Метод для изменения заказа
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Order entity)
        {
            List<Order> ordersInFile = Read().ToList();

            int index = ordersInFile.FindIndex(t => t.Id == entity.Id);

            ordersInFile[index] = entity;

            File.WriteAllText(fileName, JsonConvert.SerializeObject(ordersInFile));
        }

    }
}
