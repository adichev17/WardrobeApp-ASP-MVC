using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WardbApp.Manager.Things;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : Controller
    {
        private readonly IThingsManager _thingManager;

        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ThingController(IThingsManager thingManager, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _thingManager = thingManager;
            _environment = environment;
            _configuration = configuration;
        }


        [HttpGet("/GetAllThings/{UserId}")]
        public async Task<IActionResult> GetAllThings(string UserId)
        {
            var AllThings = await _thingManager.GetAllThing(UserId);
            if (AllThings != null)
                return Json(AllThings);

            return UnprocessableEntity();
        }


        [HttpPost("/loadImg")]
        public async Task<IActionResult> LoadImg([FromForm(Name = "file")] IFormFile ImageFile)
        {
            string wwwRootPath = _environment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png") return StatusCode(400);
            fileName = $"{fileName}{extension}";
            string path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}");
            if (System.IO.File.Exists(path)) { fileName += "_"; path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}"); }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            string url = "https://wardrobeapp.azurewebsites.net/Images/" +$"{DateTime.Now.ToString().Substring(0, 9)}." + $"{fileName}";
            var responce = new { Name = fileName, URL = url };
            return Json(responce);
        }

    }
}