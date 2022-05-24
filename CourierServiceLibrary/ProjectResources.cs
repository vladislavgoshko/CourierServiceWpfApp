using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CourierServiceLibrary.Models;
using CourierServiceLibrary.Repository;

namespace CourierServiceLibrary
{
    /// <summary>
    /// Класс для работы с ресурсами
    /// </summary>
    public class ProjectResources
    {
        private static ProjectResources resources;
        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public readonly Authentification authentification;

        private string PathUsers = $@"{Directory.GetCurrentDirectory()}\Data\Users.json";
        private string PathOrders = $@"{Directory.GetCurrentDirectory()}\Data\Orders.json";
        private string PathCouriers = $@"{Directory.GetCurrentDirectory()}\Data\Couriers.json";
        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        public readonly IRepository<User> UsersRepository;
        /// <summary>
        /// Репозиторий заказов
        /// </summary>
        public readonly IRepository<Order> OrdersRepository;
        /// <summary>
        /// Репозиторий курьеров
        /// </summary>
        public readonly IRepository<Courier> CouriersRepository;
        private ProjectResources()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data");
            if (!File.Exists(PathUsers))
                File.Create(PathUsers);
            if (!File.Exists(PathOrders))
                File.Create(PathOrders);
            if (!File.Exists(PathCouriers))
                File.Create(PathCouriers);
            UsersRepository = new UserRepository(PathUsers);
            OrdersRepository = new OrderRepository(PathOrders);
            CouriersRepository = new CourierRepository(PathCouriers);
            authentification = new Authentification(UsersRepository);
        }
        /// <summary>
        /// Singleton
        /// </summary>
        public static ProjectResources Instance
        {
            get { return resources == null ? new ProjectResources() : resources; }
        }
    }
}
