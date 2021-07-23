using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Looks;

namespace WardbApp.Manager.Looks
{
    public interface ILookManager
    {
        public Task<LookViewModel> NewLook(string UserId, LookViewModel Look);
        public Task<List<List<string>>> GetAllLooks(string UserId);
    }
}
