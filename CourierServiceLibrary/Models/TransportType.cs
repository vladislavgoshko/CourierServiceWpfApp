using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourierServiceLibrary.Models
{
    /// <summary>
    /// Перечисление типов транспорта
    /// </summary>
    public enum TransportType
    {
        [Display(Name = "Пеший")]
        Foot,
        [Display(Name = "Велосипед")]
        Bicycle,
        [Display(Name = "Машина")]
        Car
    }
}
