using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Xrm.DataAcces.Crm
{
    public interface ICrmUnitOfWork
    {
        IOrganizationService OrganisationService { get; }

        void Add(Entity entity);
        void Update(Entity entity);
        void Remove(string entityName, Guid id);
        void Remove(EntityReference entityRef);
        void Commit();
        IEnumerable<Guid> CommitEnumerable();
    }
}
