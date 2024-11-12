using JWTAuthentication.Model;
using JWTAuthentication.WebApi.Auth;
using JWTAuthentication.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JWTAuthentication.Service;

namespace JWTAuthentication.WebApi.Controllers
{
    [RoutePrefix("Member")]
    public class MembershipController : ApiController
    {
        private IMembershipService membershipService;

        public MembershipController()
        {
            this.membershipService = new MembershipService();
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult RegisterMember(Member member)
        {
            var result = this.membershipService.AddMember(member);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok("Member registered successfully. Please proceed to login. Userid is your email.");
            }

            return InternalServerError(new Exception(result.ErrorMessage));
        }

        [HttpGet]
        [Route("authenticate")]
        public IHttpActionResult AuthenticateMember(string username, string password)
        {
            var result = this.membershipService.AuthenticateMember(username, password);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(JwtAuthManager.GenerateJWTToken(username));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("profile")]
        [JwtAuthenticationFilter]
        public IHttpActionResult GetMemberProfile(string username)
        {
            var result = this.membershipService.GeMemberDetails(username);
            if (string.IsNullOrEmpty(result.ErrorMessage) 
                && result.Member !=null)
            {
                var member = result.Member;
                var memberDetails = new MemberProfile
                {
                    Name = member.Name,
                    Email = member.Email,
                    Dob = member.Dob,
                    Gender = member.Gender,
                    MobileNumber = member.MobileNumber,
                    EmailOptIn = member.EmailOptIn
                };
                return Ok(memberDetails);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("update")]
        [JwtAuthenticationFilter]
        public IHttpActionResult UpdateMember(Member member)
        {
            var result = this.membershipService.UpdateMemberDetails(member);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok("Member details updated successfully.");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
