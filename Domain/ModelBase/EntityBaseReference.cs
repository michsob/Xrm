using System;

namespace Xrm.Domain.ModelBase
{
    public struct EntityBaseReference
    {
        public string LogicalName { get; }
        public string Name { get; }
        public Guid Id { get; }

        public EntityBaseReference(string logicalName, Guid id)
        {
            LogicalName = logicalName;
            Name = string.Empty;
            Id = id;
        }

        public EntityBaseReference(string logicalName, string name, Guid id)
        {
            LogicalName = logicalName;
            Name = name;
            Id = id;
        }
    }
}
