using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Domain.DataAccessBase;

namespace Xrm.Domain.Contact
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;
        public ContactService(IUnitOfWork unitOfWork, IContactRepository contactRepository)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            if (contactRepository == null)
                throw new ArgumentNullException("contactRepository");

            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
        }

        public void UpdateContact(Contact contact)
        {
            _contactRepository.Update(contact);
            _unitOfWork.Commit();
        }

        public Contact GetContactByEmail(string email)
        {
            return _contactRepository.GetByEmail(email);
        }
    }
}
