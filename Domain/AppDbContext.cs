using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Account;
using WardbApp.Models.ClothingSeason;
using WardbApp.Models.ClothingСategory;
using WardbApp.Models.Picture;

namespace WardbApp.Domain
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> CategoryClothing { get; set; }
        public DbSet<Season> SeasonClothing { get; set; }
        public DbSet<UsersThings> UsersThings { get; set; }

    }
}
