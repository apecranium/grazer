using System.ComponentModel.DataAnnotations;

namespace Grazer.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "username is required.")]
        [Display(Name = "username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "email is required.")]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [Display(Name = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}
