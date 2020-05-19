using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public int BarID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }
        public string Review { get; set; }

    }
}
