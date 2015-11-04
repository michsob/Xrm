using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Infrastructure.DataAccess.Sql.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }

        public virtual List<Contact> Contacts { get; set; }
    }
}
