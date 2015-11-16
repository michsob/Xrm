using Xrm.Domain.ModelBase;

namespace Xrm.Domain.DataAccessBase
{
    public interface IPersistRepository
    {
        void PersistAdded(EntityBase entity);
        void PersistUpdated(EntityBase entity);
        void PersistRemoved(EntityBase entity);
    }
}
