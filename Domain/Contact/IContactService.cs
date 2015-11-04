using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Domain.Contact
{
    public interface IContactService
    {
        void UpdateContact(Contact contact);
        Contact GetContactByEmail(string email);
    }
}
