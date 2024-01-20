using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Chat
{
    public class MessageViewModel
    {

        [Required]
        public string Sender { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Message { get; set; } = null!;
    }
}
