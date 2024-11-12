using JWTAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Repository
{
    public interface IMembershipRepository
    {
        void Add(Member member);

        void Update(Member member);

        void Delete(Member member);

        Member GetByEmail(string email);
    }
}
