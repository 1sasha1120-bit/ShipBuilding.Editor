using Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Common.Repositories {
    public class AutoincrementRepository<T> : Repository<T>
        where T : class, IIdentifiable {

        public AutoincrementRepository(ICollection<T> collection) 
            : base(collection) {
        }

        public override void Add(T item) {
            item.Id = Collection.Any() ? Collection.Select(e => e.Id).Max() + 1 : 1;
            base.Add(item);
        }
    }
}
