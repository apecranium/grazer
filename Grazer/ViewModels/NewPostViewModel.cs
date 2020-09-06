using System.ComponentModel.DataAnnotations;

namespace Grazer.ViewModels
{
    public class NewPostViewModel
    {
        [Required]
        [Display(Name = "title")]
        public string Title { get; set; }

        [Display(Name = "body content")]
        public string BodyContent { get; set; }
    }
}
