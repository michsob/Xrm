using System;
using System.Collections.Generic;
using Xrm.Domain.ModelBase;

namespace Xrm.Domain.DataAccessBase
{
    public interface IUnitOfWork
    {
        void RegisterAdded(EntityBase entity, IPersistRepository repository);
        void RegisterUpdated(EntityBase entity, IPersistRepository repository);
        void RegisterRemoved(EntityBase entity, IPersistRepository repository);
        //void RegisterDeleted(EntityBaseReference entity, IPersistRepository repository);
        void Commit();
        IEnumerable<Guid> CommitEnumerable();
    }
}
