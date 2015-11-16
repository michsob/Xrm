using System.Collections.Generic;
using System.Transactions;
using Xrm.Domain.DataAccessBase;
using Xrm.Domain.ModelBase;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private Dictionary<EntityBase, IPersistRepository> Added { get; set; }
        private Dictionary<EntityBase, IPersistRepository> Modified { get; set; }
        private Dictionary<EntityBase, IPersistRepository> Removed { get; set; }

        public SqlUnitOfWork()
        {
            Added = new Dictionary<EntityBase, IPersistRepository>();
            Modified = new Dictionary<EntityBase, IPersistRepository>();
            Removed = new Dictionary<EntityBase, IPersistRepository>();
        }

        public void RegisterAdded(EntityBase entity, IPersistRepository repository)
        {
            if (!Added.ContainsKey(entity))
                Added.Add(entity, repository);
        }

        public void RegisterUpdated(EntityBase entity, IPersistRepository repository)
        {
            if (!Modified.ContainsKey(entity))
                Modified.Add(entity, repository);
        }

        public void RegisterRemoved(EntityBase entity, IPersistRepository repository)
        {
            if (!Removed.ContainsKey(entity))
                Removed.Add(entity, repository);
        }

        public void Commit()
        {
            using (var transactionScope = new TransactionScope())
            {
                foreach (var entity in Added.Keys)
                    Added[entity].PersistAdded(entity);
                foreach (var entity in Modified.Keys)
                    Modified[entity].PersistUpdated(entity);
                foreach (var entity in Removed.Keys)
                    Removed[entity].PersistRemoved(entity);
            }
        }

        //public IEnumerable<Guid> CommitEnumerable()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
