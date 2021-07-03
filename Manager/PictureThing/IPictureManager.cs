using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Picture;
using WardbApp.Models.Things;

namespace WardbApp.Manager.PictureThing
{
    public interface IPictureManager
    {
        public Task<PictureThingViewModel> Create(PictureThingViewModel pic, string UserId);
    }
}
