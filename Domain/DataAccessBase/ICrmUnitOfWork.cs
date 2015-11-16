using System;
using System.Collections.Generic;

namespace Xrm.Domain.DataAccessBase
{
    public interface ICrmUnitOfWork : IUnitOfWork
    {
        IEnumerable<Guid> CommitEnumerable();
    }
}
