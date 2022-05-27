using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using CourierServiceLibrary.Models;


namespace CourierServiceLibrary.Repository
{
    /// <summary>
    /// Репозиторий для курьеров
    /// </summary>
    public class CourierRepository : IRepository<Courier>
    {
        private readonly string fileName;
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="fileName"></param>
        public CourierRepository(string fileName)
        {
            this.fileName = fileName;
        }
        /// <summary>
        /// Метод для создания сущности курьера
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Courier entity)
        {
            List<Courier> couriersInFile = Read().ToList();
            int i = 1;
            while (couriersInFile.Any(t => t.Id == entity.Id))
                entity.Id = i++;
            couriersInFile.Add(entity);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(couriersInFile));
        }
        /// <summary>
        /// Метод для чтения данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Courier> Read()
        {
            IEnumerable<Courier> couriers = JsonConvert.DeserializeObject<IEnumerable<Courier>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Couriers.json")) ?? new List<Courier>();
            IEnumerable<Order> orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(File.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\Orders.json")) ?? new List<Order>();
            bool isChanged = false;
            foreach (Courier courier in couriers)
            {
                foreach (int orderId in courier.ListOrdersId)
                {
                    foreach (Order order in orders)
                    {
                        if (order.Id == orderId)
                        {
                            if(order.Readiness == Readiness.Completed)
                            {
                                isChanged = true;
                                courier.ListOrdersId.Remove(orderId);
                            }
                        }
                    }

                }
            }
            if(isChanged)
                File.WriteAllText(fileName, JsonConvert.SerializeObject(couriers));
            return couriers;
        }
        /// <summary>
        /// Метод для удаления сущности
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<Courier> couriersInFile = Read().ToList();
            couriersInFile.RemoveAt(couriersInFile.FindIndex(t => t.Id == id));
            foreach (Order order in ProjectResources.Instance.OrdersRepository.Read())
            {
                if (order.CourierId == id && order.Readiness == Readiness.InProgress)
                {
                    order.Readiness = Readiness.Pending;
                    order.CourierId = -1;
                    order.StartedExecuting = new DateTime();
                    ProjectResources.Instance.OrdersRepository.Update(order);
                }
            }
            File.WriteAllText(fileName, JsonConvert.SerializeObject(couriersInFile));
        }
        /// <summary>
        /// Метод для изменения сущности
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Courier entity)
        {
            List<Courier> couriersInFile = Read().ToList();

            int index = couriersInFile.FindIndex(t => t.Id == entity.Id);

            couriersInFile[index] = entity;

            File.WriteAllText(fileName, JsonConvert.SerializeObject(couriersInFile));
        }
    }
}
