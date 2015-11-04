using System;
using System.Web.Http;
using Xrm.Domain.Contact;

namespace Xrm.Presentation.RestApi.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            if (contactService == null)
                throw new ArgumentNullException("contactService");

            _contactService = contactService;
        }

        [Route("api/getcontact/{email}")]
        public Contact GetContact(string email)
        {
            return _contactService.GetContactByEmail(email);
        }
    }
}
