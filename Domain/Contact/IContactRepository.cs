using Xrm.Domain.DataAccessBase;

namespace Xrm.Domain.Contact
{
    public interface IContactRepository : IRepository<Contact>
    {
        Contact GetByEmail(string email);
    }
}
