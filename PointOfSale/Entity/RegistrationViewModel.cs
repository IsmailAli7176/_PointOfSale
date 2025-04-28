using System.ComponentModel.DataAnnotations;

namespace Register_Login.Models
{
    public class RegistrationViewmodel
    {
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]


        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        [EmailAddress(ErrorMessage = "Please Enter Valid  Email")]


        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]


        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or min 5  characters allowed.")]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Please Confrim Your Password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
