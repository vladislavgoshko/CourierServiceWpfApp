using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Models
{
    public class CourierFoot : Courier
    {
        public CourierFoot(int id, string name, string secondName, double[] address, double[] currentCoords, bool occupation) 
            : base(id, name, secondName, address, currentCoords, occupation, 7)
        {
        }
    }
}
