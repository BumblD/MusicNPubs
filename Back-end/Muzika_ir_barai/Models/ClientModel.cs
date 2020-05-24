using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsBlocked { get; set; }
        public int WaitTime { get; set; }
    }
}
