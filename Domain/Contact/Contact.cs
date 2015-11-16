using Xrm.Domain.ModelBase;

namespace Xrm.Domain.Contact
{
    public class Contact : EntityBase
    {
        public Contact(bool recordChanges = true)
            : base(recordChanges)
        { }

        private string firstname;
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                if (firstname != value) 
                {
                    firstname = value;
                    if (RecordChanges == true)
                        OnPropertyChanged("Firstname");
                }

            }
        }

        private string lastname;
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                if (lastname != value) 
                {
                    lastname = value;
                    if (RecordChanges == true)
                        OnPropertyChanged("Lastname");
                }
            }
        }

        private string emailaddress1;
        public string EmailAddress1
        {
            get
            {
                return emailaddress1;
            }
            set
            {
                if (emailaddress1 != value) 
                {
                    emailaddress1 = value;
                    if (RecordChanges == true)
                        OnPropertyChanged("EmailAddress1");
                }
            }
        }
    }
}
