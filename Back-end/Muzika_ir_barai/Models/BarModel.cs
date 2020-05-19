using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class BarModel
    {
        public int BarID { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string BankNumber { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }

    }
}
