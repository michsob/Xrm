using System;
using Xrm.Domain.ModelBase;

namespace Xrm.Domain.DataAccessBase
{
    public interface IRepository<T> where T : EntityBase
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        //void Remove(EntityBaseReference id);

        T GetBy(Guid id);
    }
}
