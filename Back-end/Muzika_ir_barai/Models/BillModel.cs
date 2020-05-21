using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public int TableNo { get; set; }
        public double PayAmount { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public bool Paid { get; set; }
        public int CustomerId { get; set; }
        public int BarId { get; set; }
    }
}
