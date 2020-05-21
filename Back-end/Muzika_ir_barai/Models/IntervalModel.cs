using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class IntervalModel
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
