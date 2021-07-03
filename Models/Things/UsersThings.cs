using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Account;
using WardbApp.Models.ClothingSeason;
using WardbApp.Models.ClothingСategory;

namespace WardbApp.Models.Picture
{
    public class UsersThings
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int SeasonId { get; set; }
        [ForeignKey(nameof(SeasonId))]
        public Season Season { get; set; }
    }
}
