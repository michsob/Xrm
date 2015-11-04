using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Domain.Contact;
using Xrm.Domain.DataAccessBase;
using Xrm.Domain.ModelBase;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    class ContactRepository
        : RepositoryBase<Contact>,
        IContactRepository
    {
        public ContactRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Contact GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Contact GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public override void PersistAdded(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void PersistRemoved(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public override void PersistUpdated(EntityBase entity)
        {
            throw new NotImplementedException();
        }
    }
}
