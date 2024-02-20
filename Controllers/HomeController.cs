using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarHub.Models;
using System.Diagnostics;

namespace SeminarHub.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        
        public IActionResult Index()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                 return RedirectToAction("All", "Seminar");
          
            }

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}