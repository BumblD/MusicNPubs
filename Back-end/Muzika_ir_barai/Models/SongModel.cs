using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? ListeningCount { get; set; }
        public int? Rating { get; set; }

        public int? Length = 150;
    }

    public class RemoveModel
    {
        public List<SongModel> Songs { get; set; }
        public List<int> BlockedIds { get; set; }
    }
}
