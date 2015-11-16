namespace Xrm.Domain.Contact
{
    public interface IContactService
    {
        void UpdateContact(Contact contact);
        Contact GetContactByEmail(string email);
    }
}
