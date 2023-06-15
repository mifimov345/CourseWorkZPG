using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkZPG.BDModels
{
    public class Journal
    {
        public int Id { get; set; }
        public string? Happening { get; set; }
        public PlayableChar? player { get; set; }
        public Evented? event_ { get; set; }
        public Item? item_ { get; set; }
        public NPC? name_ { get; set; }
    }
}
