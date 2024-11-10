using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SabeloSethu.Api.Models.User
{
    public class RegisterUserModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "MobileNumber must 10 characters long.")]
        [StringLength(10, ErrorMessage = "MobileNumber must be 10 characters.")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
        public string Password { get; set; }
    }
}
