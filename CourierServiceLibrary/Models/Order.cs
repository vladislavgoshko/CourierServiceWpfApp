using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models
{
    /// <summary>
    /// Class of order
    /// </summary>
    public class Order : IBaseEntity
    {
        public int Id { get; set; }
        public Readiness Readiness { get; set; }
        public double[] StartCoords { get; set; } = new double[2];
        public double[] EndCoords { get; set; } = new double[2];
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Weigth { get; set; }
        public int UserId { get; set; }
        public int CourierId { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartedExecuting { get; set; }
        public string Message { get; set; }
        /*
        public Order(int id, Readiness readiness, double[] startCoords, double[] endCoords, int itemId, int userId)
        {
            Id = id;
            Readiness = readiness;
            StartCoords = startCoords;
            EndCoords = endCoords;
            ItemId = itemId;
            UserId = userId;
            CourierId = -1;
        }*/
        public double GetDistance()
        {
            double x1 = StartCoords[0], y1 = StartCoords[1], x2 = EndCoords[0], y2 = EndCoords[1];
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) / Math.Sqrt(2) * 2;
        }
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
        public override string ToString()
        {
            return $"Id: {Id}, Readiness: {Readiness}, Start coords: ({StartCoords[0]}; {StartCoords[1]}), " +
                $"End coords: ({EndCoords[0]}; {EndCoords[1]}), IserId: {UserId}, CourierId: {CourierId}";
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order order = (Order)obj;
                return Id == order.Id && Readiness == order.Readiness &&
                    StartCoords == order.StartCoords && EndCoords == order.EndCoords && UserId == order.UserId && CourierId == order.CourierId;
            }
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * Readiness.GetHashCode() * StartCoords.GetHashCode() * 
                EndCoords.GetHashCode() * UserId.GetHashCode() * CourierId.GetHashCode();
        }
    }
}
