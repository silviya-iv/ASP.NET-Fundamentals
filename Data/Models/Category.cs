using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        public Category() 
        {
            Seminars = new List<Seminar>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Seminar> Seminars { get; set; } 
    }
}
//•	Has Id – a unique integer, Primary Key
//•	Has Name – string with min length 3 and max length 50 (required)
//•	Has Seminars – a collection of type Seminar
