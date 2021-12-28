using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Video_Gallary.Areas.Admin.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage ="First Name can't empty !")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can't empty !")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Email is not valid !")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage ="Please enter strong password !")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Confirm password not matched !")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
