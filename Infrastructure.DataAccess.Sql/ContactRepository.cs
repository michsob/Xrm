using System;
using System.Linq;
using Xrm.Domain.Contact;
using Xrm.Domain.DataAccessBase;
using Xrm.Domain.ModelBase;
using Xrm.Infrastructure.DataAccess.Sql.Mapping;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    public class ContactRepository
        : RepositoryBase<Contact>,
        IContactRepository
    {
        private readonly SqlContext _context;

        public ContactRepository(IUnitOfWork unitOfWork, SqlContext context)
            : base(unitOfWork)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public override Contact GetBy(Guid id)
        {
            return (from c in _context.Contacts
                    where c.ContactId == id
                    select new Contact(false)
                    {
                        Firstname = c.Firstname,
                        Lastname = c.Lastname,
                        EmailAddress1 = c.Email,
                        RecordChanges = true
                    }).FirstOrDefault();
        }

        public Contact GetByEmail(string email)
        {
            return _context.Contacts.Where(c => c.Email == email)
                    .ToList().
                    Select(c => new Contact(false)
                    {
                        Id = c.ContactId,
                        Firstname = c.Firstname,
                        Lastname = c.Lastname,
                        EmailAddress1 = c.Email,
                        RecordChanges = true
                    }).FirstOrDefault();
        }

        public override void PersistAdded(EntityBase entity)
        {
            _context.Contacts.Add(SqlMapper.ConvertToEFObject((Contact)entity));
            _context.SaveChanges();
        }

        public override void PersistRemoved(EntityBase entity)
        {
            _context.Contacts.Remove(SqlMapper.ConvertToEFObject((Contact)entity));
            _context.SaveChanges();
        }

        public override void PersistUpdated(EntityBase entity)
        {
            var result = _context.Contacts.SingleOrDefault(c => c.ContactId == entity.Id);
            if (result != null)
            {
                result.Firstname = ((Contact)entity).Firstname;
                result.Lastname = ((Contact)entity).Lastname;
                _context.SaveChanges();
            }
        }
    }
}


