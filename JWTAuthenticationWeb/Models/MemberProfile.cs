using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JWTAuthentication.WebApi
{
    public class MemberProfile
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Gender { get; set; }

        public string Dob { get; set; }

        public string EmailOptIn { get; set; }
    }
}