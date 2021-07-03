using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Domain;
using WardbApp.Models.Account;
using WardbApp.Models.Picture;
using WardbApp.Models.Things;

namespace WardbApp.Manager.PictureThing
{
    public class PictureManager: IPictureManager
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public PictureManager(UserManager<User> userManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<PictureThingViewModel> Create(PictureThingViewModel pic, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var category = await _context.CategoryClothing.FirstOrDefaultAsync(name => pic.Category == name.Name);
            var season = await _context.SeasonClothing.FirstOrDefaultAsync(name => pic.Season == name.Name);
            if (user != null && category != null && season != null)
            {
                UsersThings UsersThings = new UsersThings
                {
                    User = user,
                    Category = category,
                    Season = season,
                    Image = pic.ImageSrc
                };

                await _context.UsersThings.AddAsync(UsersThings);
                await _context.SaveChangesAsync();

                return pic;
            }
            return null;
        }
    }
}
