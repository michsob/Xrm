using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
