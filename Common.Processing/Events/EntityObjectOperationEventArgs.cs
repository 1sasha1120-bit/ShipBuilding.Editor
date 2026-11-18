using Common.Entities;
using System;

namespace Common.Processing.Events {
    public class EntityObjectOperationEventArgs : EventArgs {
        public readonly string EntityTypeName;
        public readonly Operation Operation;

        public Entity EntityObject { get; set; }
        public bool Cancel { get; set; }

        public EntityObjectOperationEventArgs(string entityTypeName, 
            Operation operation, Entity entityObject) {

            EntityObject = entityObject;
            EntityTypeName = entityTypeName;
            Operation = operation;
        }


    }
}
