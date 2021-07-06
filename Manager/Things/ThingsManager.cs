using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public ThingsManager(UserManager<User> userManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
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
    }
}
