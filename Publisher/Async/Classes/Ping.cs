using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Echantion.Classes.Server
{
    public class Ping : IResponse
    {
        public string MakeResponse()
        {
            return "Pong";
        }
    }
}
