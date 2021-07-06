using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardbApp.Models.ClothingСategory;

namespace WardbApp.Models.Things
{
    public class GetThings
    {
        public string Category { get; set; }
        public List<string> IncludeThings { get; set; }
    }
}
