using System;
using Xrm.Domain.ModelBase;

namespace Xrm.Domain.DataAccessBase
{
    public abstract class RepositoryBase<DomainType> 
        : IRepository<DomainType>,
        IPersistRepository
        where DomainType : EntityBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(IUnitOfWork  unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public void Add(DomainType entity)
        {
            _unitOfWork.RegisterAdded(entity, this);
        }

        public void Update(DomainType entity)
        {
            _unitOfWork.RegisterUpdated(entity, this);
        }

        public void Remove(DomainType entity)
        {
            _unitOfWork.RegisterRemoved(entity, this);
        }

        public abstract DomainType GetBy(Guid id);
        public abstract void PersistAdded(EntityBase entity);
        public abstract void PersistUpdated(EntityBase entity);
        public abstract void PersistRemoved(EntityBase entity);
    }
}
