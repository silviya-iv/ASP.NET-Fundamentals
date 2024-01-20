using ChatApp.Models;
using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
      
        private static IList<KeyValuePair<string,string>> messages= new List<KeyValuePair<string,string>>();

        public IActionResult Show()
        {
            if (messages.Count < 1)
            { return View(new ChatViewModel()); }

            var chatModel = new ChatViewModel()
            {
                AllMessages = messages.Select(m => new MessageViewModel
                {
                    Sender = m.Key,
                    Message = m.Value
                }).ToList()

            };
            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;

            messages.Add(new KeyValuePair<string, string>( newMessage.Sender, newMessage.Message));
            return RedirectToAction("Show");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}