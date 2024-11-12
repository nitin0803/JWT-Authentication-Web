using JWTAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.DataAccess
{
    public class MembershipConfiguration : EntityTypeConfiguration<Member>
    {
        public MembershipConfiguration()
        {
            ToTable("Members");
            HasKey(x => x.Email);
            Property(x=>x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Email).IsRequired().HasMaxLength(50);
            Property(x => x.Password).IsRequired().HasMaxLength(50);
            Property(x => x.MobileNumber).IsRequired().HasMaxLength(50);
            Property(x => x.Gender).IsRequired();
            Property(x => x.Dob).IsRequired();
        }
    }
}
