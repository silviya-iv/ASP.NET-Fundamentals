using System.ComponentModel.DataAnnotations;
using ForumApp.Common;
namespace ForumApp.ViewModel
{
    public class PostFormModel
    {
        [Required]
        [MinLength(ValidationConstants.TitleMinLength)]
        [MaxLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(ValidationConstants.ContentMinLength)]
        [MaxLength(ValidationConstants.ContentMaxLength)]
        public string Content { get; set; }
    }
}
