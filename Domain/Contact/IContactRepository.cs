using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Domain.DataAccessBase;

namespace Xrm.Domain.Contact
{
    public interface IContactRepository : IRepository<Contact>
    {
        Contact GetByEmail(string email);
    }
}
