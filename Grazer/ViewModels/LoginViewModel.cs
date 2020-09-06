using System.ComponentModel.DataAnnotations;

namespace Grazer.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "username is required.")]
        [Display(Name = "username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [Display(Name = "password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "remember me?")]
        public bool RememberMe { get; set; }
    }
}
