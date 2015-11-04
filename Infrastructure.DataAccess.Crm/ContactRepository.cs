using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using Xrm.Domain.Contact;
using Xrm.Domain.DataAccessBase;
using Xrm.Domain.ModelBase;
using Xrm.Infrastructure.DataAccess.Mapping;

namespace Xrm.Infrastructure.DataAccess.Crm
{
    public class ContactRepository
        : RepositoryBase<Contact>,
        IContactRepository
    {
        public readonly CrmEntities.TestOrganizationContext _context;

        public ContactRepository(IOrganizationService service, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _context = new CrmEntities.TestOrganizationContext(service);
        }

        public override Contact GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Contact GetByEmail(string email)
        {
            var result = (from c in _context.ContactSet
                          where c.EMailAddress1 == email
                          select new Contact(false)
                          {
                              Id = c.ContactId.Value,
                              Firstname = c.FirstName,
                              Lastname = c.LastName,
                              EmailAddress1 = c.EMailAddress1,
                              RecordChanges = true
                          });
            return result.FirstOrDefault();
        }

        public override void PersistAdded(EntityBase entity)
        {
            var createRequest = new CreateRequest
            {
                Target = ((Contact)entity).ToSdkEntityObject<Contact, CrmEntities.Contact>()
            };

            ((CrmUnitOfWork)_unitOfWork).Requests.Add(createRequest);
        }

        public override void PersistUpdated(EntityBase entity)
        {
            var updateRequest = new UpdateRequest
            {
                Target = ((Contact)entity).ToSdkEntityObject<Contact, CrmEntities.Contact>()
            };

            ((CrmUnitOfWork)_unitOfWork).Requests.Add(updateRequest);
        }

        public override void PersistRemoved(EntityBase entity)
        {
            var deleteRequest = new DeleteRequest
            {
                Target = new EntityReference(CrmEntities.Contact.EntityLogicalName, entity.Id)
            };

            ((CrmUnitOfWork)_unitOfWork).Requests.Add(deleteRequest);
        }
    }
}
