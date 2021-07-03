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

        public async Task<List<UsersThings>> GetAllThing(string UserId)
        {
            var Things = await _context.UsersThings.Where(thn => thn.UserId == UserId).ToListAsync();
            if (Things != null)
                return Things;

            return null;
        }
    }
}
