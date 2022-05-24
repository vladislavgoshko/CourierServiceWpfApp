using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CourierServiceLibrary.Models
{
    /// <summary>
    /// Класс курьера
    /// </summary>
    public class Courier : IBaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        private TransportType transportType;
        /// <summary>
        /// Свойство типа транспорта
        /// </summary>
        public TransportType TransportType
        {
            get
            {
                return transportType;
            }
            set
            {
                switch (value)
                {
                    case TransportType.Foot:
                        transportType = TransportType.Foot;
                        Speed = 7;
                        break;
                    case TransportType.Bicycle:
                        transportType = TransportType.Bicycle;
                        Speed = 16;
                        break;
                    case TransportType.Car:
                        transportType = TransportType.Car;
                        Speed = 100;
                        break;
                    default:
                        throw new NotImplementedException("Такого типа курьеров нет");
                }
            }
        }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public double[] Address { get; set; } = new double[2];
        /// <summary>
        /// Последнее местоположение
        /// </summary>
        public double[] LastLocation { get; set; } = new double[2];
        /// <summary>
        /// Список заказов назначенных курьеру
        /// </summary>
        public List<int> ListOrdersId { get; set; } = new List<int>();
        /// <summary>
        /// Скорость
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// Метод возвращающий расстояние от последней точки до указанной точки
        /// </summary>
        /// <param name="endCoords"></param>
        /// <returns>Расстояние</returns>
        public double GetDistanceFromEndLocation(double[] endCoords)
        {
            double x1 = LastLocation[0], y1 = LastLocation[1], x2 = endCoords[0], y2 = endCoords[1];
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) / Math.Sqrt(2) * 2;
        }
        /// <summary>
        /// Метод возвращающий true, если курьер свободен
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Свободен ли курьер</returns>
        public bool IsFreeAndWhen(out DateTime dateTime)
        {
            if (ListOrdersId.Count == 0)
            {
                dateTime = DateTime.Now;
                return true;
            }
            else
            {
                Order order = ProjectResources.Instance.OrdersRepository.Read().ToList().Find(t => t.Id == ListOrdersId.Last());
                if (order != null)
                {
                    double minutesForCompletingOrder = order.GetDistance() / Speed * 60;
                    dateTime = order.StartedExecuting.AddMinutes(minutesForCompletingOrder);
                }
                else
                    dateTime = DateTime.Now;
                return false;
            }
        }
        /// <summary>
        /// Тип транспорта в строковое представление
        /// </summary>
        /// <param name="transportType"></param>
        /// <returns>Строковое представление типа транспорта</returns>
        public static string TypeToString(TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.Foot:
                    return "Пеший";
                case TransportType.Bicycle:
                    return "Велосипед";
                case TransportType.Car:
                    return "Машина";
                default: return "Не определено";
            }
        }
        /// <summary>
        /// Метод возвращающий строковое представление курьера
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"\nId: {Id}, Name: {Name}, Second name: {SecondName}, \nAddress: ({Address[0]}; {Address[1]}), " +
                $"\nLast location coords: ({LastLocation[0]}; {LastLocation[1]}), Speed: {Speed}, " +
                $"\nTransport type: {TransportType}, List orders id: {string.Join(", ", ListOrdersId)}\n";
        }
        /// <summary>
        /// Метод equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Courier))
            {
                return false;
            }
            else
            {

                Courier c = (Courier)obj;
                return Id == c.Id && Name == c.Name && SecondName == c.SecondName
                    && Enumerable.SequenceEqual(Address, c.Address) && Enumerable.SequenceEqual(LastLocation, c.LastLocation) &&
                    Speed == c.Speed && Enumerable.SequenceEqual(ListOrdersId, c.ListOrdersId) &&
                    transportType == c.transportType;

            }
        }
        /// <summary>
        /// Метод GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() * Name.GetHashCode() * SecondName.GetHashCode() *
                Address.GetHashCode() * LastLocation.GetHashCode() * Speed.GetHashCode()
                * ListOrdersId.GetHashCode() * transportType.GetHashCode();
        }
    }
}
