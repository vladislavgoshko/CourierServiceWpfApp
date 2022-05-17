using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models
{
    public class Courier : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public double[] Address { get; set; } = new double[2];
        public double[] LastLocation { get; set; } = new double[2];
        public List<int> ListOrdersId { get; set; } = new List<int>();
        public bool Occupation { get; set; }
        public double Speed { get; set; }
        public Courier(int id, string name, string secondName, double[] address, double[] currentCoords, bool occupation, double speed)
        {
            Id = id;
            Name = name;
            SecondName = secondName;
            Address = address;
            LastLocation = currentCoords;
            Occupation = occupation;
            Speed = speed;
        }
        public double GetDistance(double[] endCoords)
        {
            double x1 = Address[0], y1 = Address[1], x2 = endCoords[0], y2 = endCoords[1];
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) / Math.Sqrt(2) * 2;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Second name: {SecondName}, Address: ({Address[0]}; {Address[1]}), " +
                $"Current coords: ({LastLocation[0]}; {LastLocation[1]}), Occupation: {Occupation}, Speed: {Speed}";
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Courier c = (Courier)obj;
                return Id == c.Id && Name == c.Name && SecondName == c.SecondName
                    && Address == c.Address && LastLocation == c.LastLocation
                    && Occupation == c.Occupation && Speed == c.Speed;
            }
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * Name.GetHashCode() * SecondName.GetHashCode() *
                Address.GetHashCode() * LastLocation.GetHashCode() * Occupation.GetHashCode() * Speed.GetHashCode();
        }
    }
}
