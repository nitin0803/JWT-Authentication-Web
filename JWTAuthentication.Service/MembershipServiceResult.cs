using JWTAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Service
{
    public class MembershipServiceResult
    {
        public Member Member { get; set; }
        public string ErrorMessage { get; set; }
    }
}
