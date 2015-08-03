using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryApp.UI.Models
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage="Укажите корректный Email")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "Пароль должен быть не больше 16 символов")]
        public string FirstName { get; set; }

        [StringLength(25,ErrorMessage="Пароль должен быть не больше 16 символов")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16,ErrorMessage="Пароль должен быть не больше 16 символов")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}