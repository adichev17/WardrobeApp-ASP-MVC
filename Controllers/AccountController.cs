using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WardbApp.Manager.Users;
using WardbApp.Models.Account;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountManager _AccountManager;

        public AccountController(IAccountManager accountManager)
        {
            _AccountManager = accountManager;
        }

        [Route("/reg")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _AccountManager.Register(model);

                if (user != null)
                {
                    return Json(user);
                }
                else
                {
                    return StatusCode(501);
                }
            }
            return View(model);
        }

        [HttpPost("/CreateToken")]
        public IActionResult CreateToken(JwtTokenViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = _AccountManager.CreateToken(model);
                if (user != null)
                    return Created("", user);
                else
                    return Unauthorized();
            }
            return BadRequest();
        }
    }
}