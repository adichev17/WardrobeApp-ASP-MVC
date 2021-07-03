using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardbApp.Models.Things
{
    public class PictureThingViewModel
    {
        [Required]
        public string ImageSrc { get; set; }
        public string Category { get; set; }
        public string Season { get; set; }
    }
}
