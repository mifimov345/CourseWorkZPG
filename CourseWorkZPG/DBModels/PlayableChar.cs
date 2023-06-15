using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkZPG.BDModels
{
    public class PlayableChar
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Lvl { get; set; }
        public int? Exp { get; set; }

        public int? CurLoc { get; set; }
    }
}
