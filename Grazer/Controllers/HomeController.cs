using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Grazer.Data;
using Grazer.Models;
using Grazer.ViewModels;

namespace Grazer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GrazerDbContext _context;
        private readonly IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger,
            GrazerDbContext context, IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public async Task<IActionResult> Index(int page = 1, int count = 5)
        {
            List<Post> posts;
            if (!_cache.TryGetValue("Posts", out posts))
            {
                posts = await _context.Posts.Include(p => p.Author)
                    .OrderByDescending(p => p.DatePosted).ToListAsync();
                _cache.Set("Posts", posts);
            }
            var model = new PostsViewModel
            {
                Page = page,
                Count = count,
                Total = posts.Count,
                Posts = posts.Skip((page - 1) * count).Take(count).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
