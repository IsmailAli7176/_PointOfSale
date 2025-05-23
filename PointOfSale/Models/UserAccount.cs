﻿using System.ComponentModel.DataAnnotations;

namespace Register_Login.Entity
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]


        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]

        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]


        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]

        public string Password { get; set; }
    }
}
