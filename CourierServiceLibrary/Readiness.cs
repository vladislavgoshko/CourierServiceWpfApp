using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourierServiceLibrary
{
    public enum Readiness
    {
        /// <summary>
        /// Выполнен
        /// </summary>
        Completed,
        /// <summary>
        /// Ожидает
        /// </summary>
        Pending,
        /// <summary>
        /// Отменен
        /// </summary>
        Canceled,
        /// <summary>
        /// Выполняется
        /// </summary>
        InProgress
    }
}
