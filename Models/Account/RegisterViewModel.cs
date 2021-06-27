using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardbApp.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserLogin { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
