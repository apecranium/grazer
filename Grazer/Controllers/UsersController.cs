using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Grazer.Data;
using Grazer.Models;
using Grazer.ViewModels;

namespace Grazer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly GrazerDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(ILogger<UsersController> logger,
            GrazerDbContext context,
            UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1, int count = 10)
        {
            var users = new UsersViewModel
            {
                Page = page,
                Count = count,
                Total = await _context.Users.CountAsync(),
                Users = await _context.Users.Skip((page - 1) * count).Take(count).ToListAsync()
            };
            return View(users);
        }
    }
}
