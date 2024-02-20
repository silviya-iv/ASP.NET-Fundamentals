using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;

namespace SeminarHub.Controllers
{
    public class SeminarController : BaseController
    {
        private readonly SeminarHubDbContext data;

        public SeminarController(SeminarHubDbContext _data)
        {
            this.data = _data;
        }

        public async Task<IActionResult> All()
        {
            var seminars = await data.Seminars
               .Select(s => new SeminarViewModel(

                   s.Id,
                   s.Topic,
                   s.Lecturer,
                   s.Category.Name,
                   s.Organizer.UserName,
                   s.DateAndTime
               ))
               .ToArrayAsync();

            return View(seminars);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel();
            model.Categories = await GetCategories();

            return View(model);            
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime dateAndTime;
           
            if (!DateTime.TryParseExact(
                model.DateAndTime,
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState
                    .AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {ValidationConstants.DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var seminar = new Seminar()
            {
                Topic=model.Topic, 
                Lecturer=model.Lecturer,
                Details=model.Details,
                OrganizerId = GetUserId(),
                DateAndTime=dateAndTime,
                Duration=model.Duration,
                CategoryId=model.CategoryId                
            };

            await data.Seminars.AddAsync(seminar);
            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarFormViewModel()
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details=seminar.Details,
                DateAndTime = seminar.DateAndTime.ToString(ValidationConstants.DateFormat),
                Duration=seminar.Duration,
                CategoryId = seminar.CategoryId
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime dateAndTime;
           

            if (!DateTime.TryParseExact(
                model.DateAndTime,
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState
                    .AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {ValidationConstants.DateFormat}");
            }

            
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = dateAndTime;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;
                     

            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminar = await data.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!seminar.SeminarsParticipants.Any(sp => sp.ParticipantId == userId))
            {
                seminar.SeminarsParticipants.Add(new SeminarParticipant()
                {
                    SeminarId = seminar.Id,
                    ParticipantId = userId
                });

                await data.SaveChangesAsync();
                return RedirectToAction("Joined");
            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.SeminarsParticipants
                .Where(sp => sp.ParticipantId == userId)
                .Select(sp => new SeminarViewModel(
                    sp.SeminarId,
                    sp.Seminar.Topic,
                    sp.Seminar.Lecturer,
                    sp.Seminar.Category.Name,                                     
                    sp.Seminar.Organizer.UserName,
                    sp.Seminar.DateAndTime
                    ))
                .ToListAsync();

            return View(model);
        }
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await data.Seminars
                .Where(s => s.Id == id)
                .Include(s=> s.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var sp = seminar.SeminarsParticipants
                .FirstOrDefault(sp => sp.ParticipantId == userId);

            if (sp == null)
            {
                return BadRequest();
            }

            seminar.SeminarsParticipants.Remove(sp);

            await data.SaveChangesAsync();

            return RedirectToAction("Joined");
        }


        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer=s.Lecturer,
                    Details=s.Details,
                    DateAndTime = s.DateAndTime.ToString(ValidationConstants.DateFormat),
                    Organizer = s.Organizer.UserName,
                    Category = s.Category.Name,
                    Duration=s.Duration
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await data.Seminars.FindAsync(id);
            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();
            if (userId != seminar.OrganizerId)
            {
                return Unauthorized();
            }
            
            SeminarDeleteModel model = new SeminarDeleteModel
            {
                Id = seminar.Id,
                Topic = seminar.Topic,
               DateAndTime=seminar.DateAndTime
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDeleteModel model)
        {
            var seminar = await data.Seminars.FindAsync(model.Id);

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();
            if (userId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            data.Seminars.Remove(seminar);
            await data.SaveChangesAsync();
            return RedirectToAction("All");
        }


        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
               .Select(t => new CategoryViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
    }
}
