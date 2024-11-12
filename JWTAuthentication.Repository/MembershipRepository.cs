using JWTAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.DataAccess;

namespace JWTAuthentication.Repository
{
    public class MembershipRepository : IMembershipRepository, IDisposable
    {
        MembershipContext membershipDBContext;

        public MembershipRepository()
        {
            if(this.membershipDBContext == null)
            this.membershipDBContext = new MembershipContext();
        }

        public void Add(Member member)
        {
            this.membershipDBContext.Members.Add(member);
            this.membershipDBContext.SaveChanges();
        }

        public void Update(Member member)
        {
            var existingMember = this.GetByEmail(member.Email);

            if (existingMember != null)
            {
                // update details except username and password
                existingMember.Name = member.Name;
                existingMember.Dob = member.Dob;
                existingMember.Gender = member.Gender;
                existingMember.MobileNumber = member.MobileNumber;
                existingMember.EmailOptIn = member.EmailOptIn;

                this.membershipDBContext.SaveChanges();
            }
        }

        public void Delete(Member member)
        {
            this.membershipDBContext.Members.Remove(member);
            this.membershipDBContext.SaveChanges();
        }

        public Member GetByEmail(string email)
        {
            var result = this.membershipDBContext.Members.Where(x=>string.Equals(x.Email,email)).FirstOrDefault();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(this.membershipDBContext !=null)
                {
                    this.membershipDBContext.Dispose();
                }
            }
        }
    }
}
