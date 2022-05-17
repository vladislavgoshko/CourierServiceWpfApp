using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierServiceLibrary.Repository
{
    public interface IRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> Read(); 
        void Create(T entity);
        void Delete(int id);
    }
}
