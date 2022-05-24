using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary
{
    /// <summary>
    /// Интерфейс для Id сущностей
    /// </summary>
    public interface IBaseEntity
    {
        int Id { get; }
    }
}
