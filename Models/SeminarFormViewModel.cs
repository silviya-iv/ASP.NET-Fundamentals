using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    using static SeminarHub.Data.ValidationConstants;
    public class SeminarFormViewModel
    {
        public SeminarFormViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Lecturer { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Details { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        public string DateAndTime { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(DurationMin,DurationMax, ErrorMessage =DurationErrorMessage)]
        public int Duration { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
