using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WardbApp.Domain;
using WardbApp.Models.Account;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = MVCJwtToken.AuthSchemes)]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;

        public AuthController(SignInManager<User> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet("/GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);
            if (user != null)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                return Json(new UserModelReturn { Name = user.Name, Email = user.Email, Id = user.Id, PhoneNumber = user.PhoneNumber });
            }
            return Unauthorized();
        }
    }
}