using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Account;
using WardbApp.Models.Picture;

namespace WardbApp.Models.Looks
{
    public class LookModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int LookId { get; set; }
        [ForeignKey(nameof(LookId))]
        public  TableLooksUser Look { get; set; }
        public int ThingId { get; set; }
        [ForeignKey(nameof(ThingId))]
        public UsersThings Thing { get; set; }
        public string ImageURI { get; set; }
    }
}
