using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.Picture;
using WardbApp.Models.Things;

namespace WardbApp.Manager.Things
{
    public interface IThingsManager
    {
        public Task<List<GetThings>> GetAllThing(string UserId);
        public Task<object> UploadingAnImage(IFormFile ImageFile, string Category, string Season, string UserId);
        public bool CheckingForCorrectnessExtension(IFormFile ImageFile);
    }
}
