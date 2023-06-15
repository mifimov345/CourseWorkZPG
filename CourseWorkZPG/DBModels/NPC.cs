using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkZPG.BDModels
{
    public class NPC
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Item? Reward { get; set; }
        public Location? Location_ { get; set; }
    }
}
