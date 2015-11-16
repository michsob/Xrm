using Xrm.Infrastructure.DataAccess.Sql.Models;

namespace Xrm.Infrastructure.DataAccess.Sql.Mapping
{
    public class SqlMapper
    {
        public static Contact ConvertToEFObject(Xrm.Domain.Contact.Contact contact)
        {
            return new Contact
            {
                ContactId = contact.Id,
                Firstname = contact.Firstname,
                Lastname = contact.Lastname,
                Email = contact.EmailAddress1
            };
        }
    }
}
