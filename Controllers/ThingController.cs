using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WardbApp.Domain;
using WardbApp.Manager.Things;
using WardbApp.Models;
using WardbApp.Models.Account;
using WardbApp.Models.Picture;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : Controller
    {
        private readonly IThingsManager _thingManager;

        public ThingController(IThingsManager thingManager)
        {
            _thingManager = thingManager;
        }


        [HttpGet("/GetAllThings/{UserId}")]
        public async Task<IActionResult> GetAllThings(string UserId)
        {
            var AllThings = await _thingManager.GetAllThing(UserId);
            if (AllThings != null)
                return Json(AllThings);

            return UnprocessableEntity();
        }


        [HttpPost("/loadImg/{UserId}")]
        public async Task<IActionResult> LoadImg([FromForm(Name = "file")] IFormFile ImageFile, [FromForm(Name = "category")] string Category, [FromForm(Name = "season")] string Season, string UserId)
        {
            if (!_thingManager.CheckingForCorrectnessExtension(ImageFile))
                return BadRequest();

            var IsLoad = await _thingManager.UploadingAnImage(ImageFile, Category, Season, UserId);
            if (IsLoad != null)
                return Json(IsLoad);

            return UnprocessableEntity();
        }

    }
}