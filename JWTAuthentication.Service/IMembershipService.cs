using JWTAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Service
{
    public interface IMembershipService
    {
        MembershipServiceResult AddMember(Member member);

        MembershipServiceResult AuthenticateMember(string userId, string password);

        MembershipServiceResult GeMemberDetails(string userId);

        MembershipServiceResult UpdateMemberDetails(Member member);
    }
}
