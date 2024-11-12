using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Model
{
    public class MemberValidation : AbstractValidator<Member>
    {
        public MemberValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required field.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required field.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required field.");
            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("MobileNumber is required field.");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required field."); 
            RuleFor(x => x.Dob).NotEmpty().WithMessage("Date of Birth is required field.");
        }
    }
}
