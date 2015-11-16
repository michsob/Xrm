using System.Data.Entity;
using Xrm.Infrastructure.DataAccess.Sql.Models;

namespace Xrm.Infrastructure.DataAccess.Sql
{
    public class SqlContext : DbContext
    {
        public SqlContext()
            :base("name=SqlContextString")
        {
            Database.SetInitializer<SqlContext>(new SqlInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
