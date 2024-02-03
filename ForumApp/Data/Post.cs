using System.ComponentModel.DataAnnotations;
using ForumApp.Common;
namespace ForumApp.Data
{

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}

