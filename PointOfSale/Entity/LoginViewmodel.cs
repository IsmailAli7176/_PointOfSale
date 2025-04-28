using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Register_Login.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        [DisplayName("Username or Email")]


        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5  characters allowed.")]

        public string Password { get; set; }
    }
}
