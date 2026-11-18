using Common.Interfaces;
using Common.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Repositories
{
    public class Repository<T> : IRepository<T>
            where T : class, IIdentifiable {

        protected ICollection<T> Collection { get; } // internal

        public Repository(ICollection<T> collection) {
            //
            this.Collection = collection;
        }

        public virtual IEnumerable<T> GetAll() {
            return Collection;
        }

        public virtual T GetById(int id) {
            var item = Collection.FirstOrDefault(e => e.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"No {typeof(T).Name} with Id {id} was found in the repository.");
            }
            return item;
        }

        public virtual void Add(T item) {
            //
            Collection.Add(item);
        }

        public virtual void Update(T item) {
            //throw new NotImplementedException();
            //throw new NotSupportedException();
            T currentItem = GetById(item.Id);
            if (currentItem != null) {
                Type type = item.GetType();
                foreach (var prop in type.GetProperties()) {
                    if (item is IKeyable && prop.Name == "Key") {
                        continue;
                    }
                    prop.SetValue(currentItem, prop.GetValue(item));
                }
            }
        }

        public virtual void Delete(int id) {
            T item = GetById(id);
            if (item != null)
                Collection.Remove(item);
        }

    }
}
