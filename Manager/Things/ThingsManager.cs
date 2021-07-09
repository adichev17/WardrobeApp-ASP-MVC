using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Domain;
using WardbApp.Models.Account;
using WardbApp.Models.Picture;
using WardbApp.Models.Things;

namespace WardbApp.Manager.Things
{
    public class ThingsManager : IThingsManager
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ThingsManager(IWebHostEnvironment environment, IConfiguration configuration,UserManager<User> userManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
            _configuration = configuration;
        }

        public bool CheckingForCorrectnessExtension(IFormFile ImageFile)
        {
            string extension = Path.GetExtension(ImageFile.FileName);
            if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png") return false;
            return true;
        }

        public async Task<List<GetThings>> GetAllThing(string UserId)
        {
            var result = new List<GetThings>();
            var ThingsOfUser = await _context.UsersThings.Where(user => user.UserId == UserId).ToListAsync();
            foreach (var category in _context.CategoryClothing)
            {
                List<string> ThingsForCategory = ThingsOfUser
                    .Where(cat => cat.CategoryId == category.Id)
                    .Select(thing => thing.Image).ToList();
                if (ThingsForCategory.Count != 0)
                {
                    var IncludeThings = new GetThings
                    {
                        Category = category.Name,
                        IncludeThings = ThingsForCategory,
                    };
                    result.Add(IncludeThings);
                }
            }
            if (result != null)
                return result;
            return null;
        }

        public async Task<object> UploadingAnImage(IFormFile ImageFile,  string Category, string Season, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return null;

            string wwwRootPath = _environment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = $"{fileName}{extension}";
            string path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}");
            if (System.IO.File.Exists(path)) { fileName += "_"; path = Path.Combine(@$"{wwwRootPath}/Images/{fileName}"); }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            string url = "https://wardrobeapp.azurewebsites.net/Images/" + $"{fileName}";
            var responce = new { Name = fileName, URL = url };


            var category = await _context.CategoryClothing.FirstOrDefaultAsync(name => Category == name.Name);
            var season = await _context.SeasonClothing.FirstOrDefaultAsync(name => Season == name.Name);
            if (category != null && season != null)
            {
                UsersThings UsersThings = new UsersThings
                {
                    User = user,
                    Category = category,
                    Season = season,
                    Image = responce.URL,
                };

                await _context.UsersThings.AddAsync(UsersThings);
                await _context.SaveChangesAsync();

            }
            else { return null; }

            return responce;
        }
    }
}
