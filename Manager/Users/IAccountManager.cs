using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Account;

namespace WardbApp.Manager.Users
{
    public interface IAccountManager
    {
        public Task<User> Register(RegisterViewModel model);
        public TokenViewModel CreateToken(JwtTokenViewModel model);
    }
}
