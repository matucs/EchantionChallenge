using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Echantion.Classes.Server
{
    public class Bye : IResponse
    {
        public string MakeResponse()
        {
            return "Bye" + System.Environment.NewLine +"The connection is closed.";
        }
    }
}
