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

            //var user = await _userManager.FindByIdAsync(UserId);
            //if (user == null)
            //    return Unauthorized();

            //string wwwRootPath = _environment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            //string extension = Path.GetExtension(ImageFile.FileName);
            //if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png") return BadRequest();
            //fileName = $"{fileName}{extension}";
            //string path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}");
            //if (System.IO.File.Exists(path)) { fileName += "_"; path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}"); }
            //using (var fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await ImageFile.CopyToAsync(fileStream);
            //}
            //string url = "https://wardrobeapp.azurewebsites.net/Images/" + $"{fileName}";
            //var responce = new { Name = fileName, URL = url };


            //var category = await _context.CategoryClothing.FirstOrDefaultAsync(name => Category == name.Name);
            //var season = await _context.SeasonClothing.FirstOrDefaultAsync(name => Season == name.Name);
            //if (category != null && season != null)
            //{
            //    UsersThings UsersThings = new UsersThings
            //    {
            //        User = user,
            //        Category = category,
            //        Season = season,
            //        Image = responce.URL,
            //    };

            //    await _context.UsersThings.AddAsync(UsersThings);
            //    await _context.SaveChangesAsync();

            //}
            //else { return UnprocessableEntity(); }

            //return Json(responce);
        }

    }
}