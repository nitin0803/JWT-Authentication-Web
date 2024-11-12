using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.Model;

namespace JWTAuthentication.DataAccess
{
    public class MembershipContext : DbContext
    {
        public MembershipContext() 
            : base("name=MemberDBConnectionString")
        {
            Database.SetInitializer<MembershipContext>(new DropCreateDatabaseIfModelChanges<MembershipContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MembershipConfiguration());
        }

        public DbSet<Member> Members { get; set; }
    }
}
