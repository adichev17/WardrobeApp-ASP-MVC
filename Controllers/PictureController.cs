using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WardbApp.Manager.PictureThing;
using WardbApp.Models.Things;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : Controller
    {
        private readonly IPictureManager _pictureManager;

        public PictureController(IPictureManager pictureManager)
        {
            _pictureManager = pictureManager;
        }


        [HttpPost("/CreateThing/{UserId}")]
        [Authorize]
        public async Task<IActionResult> CreateThing(PictureThingViewModel model, string UserId)
        {
            var NewThing = await _pictureManager.Create(model, UserId);
            if (NewThing != null)
                return Json(NewThing);

            return UnprocessableEntity();
        }
    }
}