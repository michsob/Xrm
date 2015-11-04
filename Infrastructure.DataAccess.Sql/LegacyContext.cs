using Xrm.Infrastructure.DataAccess.Sql.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    public class LegacyContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
