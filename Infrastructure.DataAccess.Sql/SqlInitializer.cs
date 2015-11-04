using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm.Infrastructure.DataAccess.Sql.Models;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    public class SqlInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LegacyContext>
    {
        protected override void Seed(LegacyContext context)
        {
            var accounts = new List<Account>
            {
                new Account {AccountId = Guid.NewGuid(), Name = "Willson" },
                new Account {AccountId = Guid.NewGuid(), Name = "Amazon" },
                new Account {AccountId = Guid.NewGuid(), Name = "Marvel" },
                new Account {AccountId = Guid.NewGuid(), Name = "Tesco" }
            };
            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();

            var contacts = new List<Contact>
            {
                new Contact {ContactId = Guid.NewGuid(), Firstname = "Yan", Lastname = "Norman", AcountId = accounts[1].AccountId},
                new Contact {ContactId = Guid.NewGuid(), Firstname = "John", Lastname = "Alonso", AcountId = accounts[1].AccountId},
                new Contact {ContactId = Guid.NewGuid(), Firstname = "Anna", Lastname = "Smith", AcountId = accounts[2].AccountId},
                new Contact {ContactId = Guid.NewGuid(), Firstname = "Arthur", Lastname = "Anand", AcountId = accounts[3].AccountId}
            };
            contacts.ForEach(c => context.Contacts.Add(c));
            context.SaveChanges();
        }
    }
}
