using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Domain;
using WardbApp.Models.Account;
using WardbApp.Models.Looks;

namespace WardbApp.Manager.Looks
{
    public class LookManager : ILookManager
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public LookManager(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        private async Task<User> AuthenticationUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return null;

            return user;
        }

        public async Task<List<List<string>>> GetAllLooks(string UserId)
        {
            var user = await AuthenticationUser(UserId);
            if (user == null)
                return null;

            List<List<string>> AllLooksForUser = new List<List<string>>();

            var IdLooksUser = await _context.TableLook
                .Where(us => us.User == user)
                .Select(look => look.Id)
                .ToListAsync();

            foreach (var id in IdLooksUser)
            {
                var Look = await _context.UsersLooks
                    .Where(look => look.LookId == id)
                    .Select(look => look.ImageURI)
                    .ToListAsync();

                if (Look.Count != 0)
                    AllLooksForUser.Add(Look);
            }
            return AllLooksForUser;
        }

        public async Task<LookViewModel> NewLook(string UserId, LookViewModel Look)
        {
            var user = await AuthenticationUser(UserId);
            if (user == null || Look.ImagesURI.Count == 0)
                return null;

            var TableLooksUser = new TableLooksUser
            {
                User = user,
                DateTime = DateTime.Now.AddHours(3),
            };

            await _context.TableLook.AddAsync(TableLooksUser);
            await _context.SaveChangesAsync();

            var LookFromTableLooks = await _context.TableLook.OrderBy(user => user.Id).LastOrDefaultAsync(user => user.UserId == UserId);

            foreach (var uri in Look.ImagesURI)
            {
                var thing = await _context.UsersThings.FirstOrDefaultAsync(thing => thing.Image == uri);

                var NewElementLook = new LookModel
                {
                    User = user,
                    ImageURI = uri,
                    Look = LookFromTableLooks,
                    Thing = thing,
                };
              await _context.UsersLooks.AddAsync(NewElementLook);
            }
            
            await _context.SaveChangesAsync();
            return Look;
        }
    }
}
