using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Model
{
    public enum EnumGender
    {
        [Display(Name = "Not specified")]
        Undefined,

        [Display(Name ="Male")]
        Male,

        [Display(Name = "Female")]
        Female
    }
}
