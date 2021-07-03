using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WardbApp.Manager.Things;

namespace WardbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : Controller
    {
        private readonly IThingsManager _thingManager;

        public ThingController(IThingsManager thingManager)
        {
            _thingManager = thingManager;
        }


        [HttpGet("/GetAllThings/{UserId}")]
        public async Task<IActionResult> GetAllThings(string UserId)
        {
            var AllThings = await _thingManager.GetAllThing(UserId);
            if (AllThings != null)
                return Json(AllThings);

            return UnprocessableEntity();
        }

    }
}