using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models
{
    public class CourierBike : Courier
    {
        public CourierBike(int id, string name, string secondName, double[] address, double[] currentCoords, bool occupation) 
            : base(id, name, secondName, address, currentCoords, occupation, 30)
        {
        }
    }
}
