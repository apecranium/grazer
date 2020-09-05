using System.ComponentModel.DataAnnotations;

namespace Grazer.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "email is required.")]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }
    }
}
