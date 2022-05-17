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
    public class ProjectResources
    {
        private static ProjectResources resources;
        public readonly Authentification authentification;

        private string PathUsers = $@"{Directory.GetCurrentDirectory()}\Data\Users.json";
        private string PathOrders = $@"{Directory.GetCurrentDirectory()}\Data\Orders.json";
        private string PathCouriers = $@"{Directory.GetCurrentDirectory()}\Data\Couriers.json";

        public readonly IRepository<User> UsersRepository;
        public readonly IRepository<Order> OrdersRepository;
        ProjectResources()
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
            authentification = new Authentification(UsersRepository);
        }
        
        public static ProjectResources Instance
        {
            get { return resources == null ? new ProjectResources() : resources; }
        }
    }
}
