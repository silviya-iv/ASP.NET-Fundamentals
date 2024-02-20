using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {

        [Required]
        [ForeignKey(nameof(Seminar))]
        public int SeminarId { get; set; }
        
        public Seminar Seminar { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Participant))]
        public string ParticipantId { get; set; } = string.Empty;

        public IdentityUser Participant { get; set; } = null!;
    }
}
//•	Has SeminarId – integer, PrimaryKey, foreign key (required)
//•	Has Seminar – Seminar
//•	Has ParticipantId – string, PrimaryKey, foreign key (required)
//•	Has Participant – IdentityUser
