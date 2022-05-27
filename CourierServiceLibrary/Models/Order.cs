using System;
using System.Linq;

namespace CourierServiceLibrary.Models
{
    /// <summary>
    /// Class of order
    /// </summary>
    public class Order : IBaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Состояние заказа
        /// </summary>
        public Readiness Readiness { get; set; }
        /// <summary>
        /// Начальная точка выполнения заказа
        /// </summary>
        public double[] StartCoords { get; set; } = new double[2];
        /// <summary>
        /// Конечная точка выполнения заказа
        /// </summary>
        public double[] EndCoords { get; set; } = new double[2];
        /// <summary>
        /// Высота посылки
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Ширина посылки
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Длина посылки
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// Вес посылки
        /// </summary>
        public double Weigth { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id курьера
        /// </summary>
        public int CourierId { get; set; }
        /// <summary>
        /// Вреся создания заказа
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Время начала выполнения заказа
        /// </summary>
        public DateTime StartedExecuting { get; set; }
        /// <summary>
        /// Сообщение от администрации
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Метод возвращающий расстояние между начальной и конечной точками
        /// </summary>
        /// <returns></returns>
        public double GetDistance()
        {
            double x1 = StartCoords[0], y1 = StartCoords[1], x2 = EndCoords[0], y2 = EndCoords[1];
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) / Math.Sqrt(2) * 2;
        }
        /// <summary>
        /// Статический метод возвращающий строковое представление состояния заказа
        /// </summary>
        /// <returns></returns>
        public string ReadinessToString()
        {
            switch (Readiness)
            {
                case Readiness.Pending:
                    return "Ожидает";
                case Readiness.Completed:
                    return "Выполнен";
                case Readiness.InProgress:
                    return "Выполняется";
                case Readiness.Canceled:
                    return "Отменен";
                    default:
                    return "Не определено";
            }
        }
        /// <summary>
        /// Метод ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Readiness: {Readiness}, Start coords: ({StartCoords[0]}; {StartCoords[1]}), " +
                $"End coords: ({EndCoords[0]}; {EndCoords[1]}), IserId: {UserId}, CourierId: {CourierId}";
        }
        /// <summary>
        /// Метод Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order order = (Order)obj;
                return Id == order.Id && Readiness == order.Readiness && Enumerable.SequenceEqual(EndCoords, order.EndCoords) &&
                    Enumerable.SequenceEqual(StartCoords, order.StartCoords) && UserId == order.UserId && CourierId == order.CourierId;
            }
        }
        /// <summary>
        /// Метод GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() * Readiness.GetHashCode() * StartCoords.GetHashCode() * 
                EndCoords.GetHashCode() * UserId.GetHashCode() * CourierId.GetHashCode();
        }
    }
}
