using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class ManejadorRtaToken
    {
        public bool Estado { get; set; } = false;
        public string Msn { get; set; } = "Error";
        public Object Rta { get; set; }
        public string Token { get; set; }
    }
}
