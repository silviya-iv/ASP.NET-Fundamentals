using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Chat
{
    public class ChatViewModel
    {
        public ChatViewModel() 
        {
           this.AllMessages = new HashSet<MessageViewModel>();
        }

        [Required]
        public MessageViewModel CurrentMessage { get; set; } = null!;

        [Required]
        public ICollection<MessageViewModel> AllMessages { get; set; }
    }
}
