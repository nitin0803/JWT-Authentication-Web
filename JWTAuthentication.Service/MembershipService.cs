using System;
using System.Text;
using JWTAuthentication.Model;
using JWTAuthentication.Repository;

namespace JWTAuthentication.Service
{
    public class MembershipService : IMembershipService
    {
        private IMembershipRepository membershipRepository;

        public MembershipService()
        {
            this.membershipRepository = new MembershipRepository();
        }

        public MembershipServiceResult AddMember(Member member)
        {
            var validationResult = this.ValidateMember(member);
            if (!string.IsNullOrEmpty(validationResult))
            {
                return new MembershipServiceResult
                {
                    ErrorMessage = validationResult
                };
            }

            try
            {
                this.membershipRepository.Add(member);
                return new MembershipServiceResult
                {
                    Member = member
                };
            }
            catch (Exception ex)
            {
                return new MembershipServiceResult
                {
                    ErrorMessage = string.Format("Error occured in adding member! Please ensure member is not already registered with userid '{0}' ", member.Email)
                };
            }
        }

        public MembershipServiceResult AuthenticateMember(string userId, string password)
        {
            try
            {
                var member = this.membershipRepository.GetByEmail(userId);
                if (member == null)
                {
                    throw new Exception(string.Format("Userid '{0}' not found as registered member!", userId));
                }

                if (member.Password.Equals(password))
                {
                    return new MembershipServiceResult
                    {
                        Member = member
                    };
                }
                throw new Exception("Provided password is wrong!");
            }
            catch (Exception ex)
            {
                return new MembershipServiceResult
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public MembershipServiceResult GeMemberDetails(string userId)
        {
            try
            {
                var member = this.membershipRepository.GetByEmail(userId);
                if (member == null)
                {
                    throw new Exception(string.Format("Userid '{0}' not found to update!",userId));
                }

                return new MembershipServiceResult
                {
                    Member = member
                };
            }
            catch (Exception ex)
            {
                return new MembershipServiceResult
                {
                    ErrorMessage = ex.Message
                };
            }
        }


        public MembershipServiceResult UpdateMemberDetails(Member member)
        {
            try
            {
                var currentDetails = this.GeMemberDetails(member.Email);
                if (currentDetails.Member == null)
                {
                    throw new Exception(currentDetails.ErrorMessage);
                }

                this.membershipRepository.Update(member);

                return new MembershipServiceResult
                {
                    Member = member
                };
            }
            catch (Exception ex)
            {
                return new MembershipServiceResult
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        private string ValidateMember(Member member)
        {
            var validator = new MemberValidation();
            var results = validator.Validate(member);
            if(!results.IsValid)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in results.Errors)
                {
                    stringBuilder.Append(item.ErrorMessage);
                }
                return stringBuilder.ToString();
            }

            return string.Empty;
        }
    }
}
