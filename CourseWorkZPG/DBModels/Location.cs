using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkZPG.BDModels
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Item> Items_ { get; set; } = new List<Item>();
        public List<NPC> NPC_ { get; set; } = new List<NPC>();
    }
}
