using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WardbApp.Manager.Looks;
using WardbApp.Manager.Things;
using WardbApp.Models.Looks;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookController : Controller
    {
        private readonly ILookManager _lookManager;

        public LookController(ILookManager lookManager)
        {
            _lookManager = lookManager;
        }

        [HttpPost("/AddLook/{UserId}")]
        public async Task<IActionResult> AddLook(string UserId, LookViewModel NewLook)
        {
            var IsAddedNewLook = await _lookManager.NewLook(UserId, NewLook);
            if (IsAddedNewLook == null)
                return UnprocessableEntity();
            
            return Ok();
        }

        [HttpGet("/GetAllLooks/{UserId}")]
        public async Task<IActionResult> GetAllLooks(string UserId)
        {
            var AllLooks = await _lookManager.GetAllLooks(UserId);
            if (AllLooks == null)
                return UnprocessableEntity();

            return Json(AllLooks);
        }
    }
}