
using SeminarHub.Data;
using SeminarHub.Data.Models;

namespace SeminarHub.Models
{
    public class SeminarViewModel
    {

        public SeminarViewModel(
           int id,
           string topic,
           string lecturer,
           string category,
           string organizer,
           DateTime dateAndTime)
        {
            Id = id;
            Topic = topic;
            Lecturer = lecturer;
            Category = category;
            Organizer = organizer;
            DateAndTime = dateAndTime.ToString(ValidationConstants.DateFormat);
        }

        public int Id { get; set; }

        public string Topic { get; set; } 

        public string Lecturer { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Organizer { get; set; }

        public string DateAndTime { get; set; }
    }
}
