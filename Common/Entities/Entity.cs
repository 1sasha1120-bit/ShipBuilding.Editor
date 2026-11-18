using Common.Interfaces;
using System;

namespace Common.Entities
{
    [Serializable]
    public abstract class Entity : IKeyable, IIdentifiable
    {
        public int Id { get; set; }

        //public virtual string Key => $"{this.GetType().Name} {Id}";

        public abstract string Key { get; }

        public override string ToString() //
        {
            return Key;
        }
    }
}
