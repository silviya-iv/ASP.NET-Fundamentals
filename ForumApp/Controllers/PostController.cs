using ForumApp.Data;
using ForumApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDbContext _data;

        public PostController(ForumDbContext data)
        {
            _data = data;
        }

        public async Task<IActionResult> All()
        {
            var posts = await _data.Posts
                .Select(p => new PostViewModel()
                {
                  Id=p.Id.ToString(),
                   Title= p.Title,
                   Content= p.Content
                })
                .ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content
            }; 
            await _data.Posts.AddAsync(post);
            await _data.SaveChangesAsync();
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var post = await _data.Posts.FirstOrDefaultAsync(p=>p.Id.ToString()==id);
            return View(new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content
            }
            );

        }

        [HttpPost]
        public async Task<IActionResult> Edit (string id,  PostFormModel model)
        {
            var post = await _data.Posts.FirstOrDefaultAsync(p => p.Id.ToString() == id);
            post.Title = model.Title;
            post.Content = model.Content;

            await _data.SaveChangesAsync();
            return RedirectToAction("All");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _data.Posts.FirstOrDefaultAsync(p => p.Id.ToString() == id);
             _data.Remove(post);
            await _data.SaveChangesAsync();
            return RedirectToAction("All");

        }
    }
}
