using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Infrastructure.DataAccess.Sql.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Guid AcountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
