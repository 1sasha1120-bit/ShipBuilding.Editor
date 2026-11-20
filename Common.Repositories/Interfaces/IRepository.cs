using Common.Interfaces;
using System.Collections.Generic;

namespace Common.Repositories.Interfaces {
    public interface IRepository<T> where T : class, IIdentifiable {
        IEnumerable<T> GetAll();
        /// <summary>Запис за ідентифікатором або null, якщо його немає.</summary>
        T? GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
