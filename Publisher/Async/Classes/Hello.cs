using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Echantion.Classes.Server
{
    public class Hello : IResponse
    {
        public string MakeResponse()
        {
            Thread.Sleep(1000);
            return "Hi";
        }
    }
}
