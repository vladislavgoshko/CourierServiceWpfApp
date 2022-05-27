using System.Collections.Generic;

namespace CourierServiceLibrary.Repository
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IBaseEntity
    {
        /// <summary>
        /// Метод для чтения данных
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Read(); 
        /// <summary>
        /// Метод для создания сущности
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);
        /// <summary>
        /// Метод для удаления сущности
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// Метод для изменения сущности
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
    }
}
