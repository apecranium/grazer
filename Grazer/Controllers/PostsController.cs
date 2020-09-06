using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Grazer.Data;
using Grazer.Models;
using Grazer.ViewModels;

namespace Grazer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly GrazerDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly UserManager<User> _userManager;

        public PostsController(ILogger<PostsController> logger,
            GrazerDbContext context,
            UserManager<User> userManager,
            IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var posts = new PostsViewModel
            {
                Page = page,
                Count = count,
                Total = await _context.Posts.CountAsync(),
                Posts = await _context.Posts.Skip((page - 1) * count).Take(count).ToListAsync()
            };
            return View(posts);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    BodyContent = model.BodyContent,
                    DatePosted = DateTime.Now
                };
                post.Author = await _userManager.GetUserAsync(User);
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                _cache.Remove("Posts");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
