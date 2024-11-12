using System.ComponentModel.DataAnnotations;
namespace JWTAuthentication.Model
{
    public class Member
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Dob { get; set; }

        public string EmailOptIn { get; set; }
    }
}
