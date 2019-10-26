using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Echantion.Classes.Server
{
    abstract class Creator
    {
        public abstract IResponse Instance();
    }
    class CreatorNull : Creator
    {
        public override IResponse Instance()
        {
            return new Null();
        }
    }
    class CreatorHello : Creator
    {
        public override IResponse Instance()
        {
            return new Hello();
        }
    }
    class CreatorBye : Creator
    {
        public override IResponse Instance()
        {
            return new Bye();
        }
    }
    class CreatorPing : Creator
    {
        public override IResponse Instance()
        {
            return new Ping();
        }
    } class CreatorException : Creator
    {
        public override IResponse Instance()
        {
            return new InvalidExeption();
        }
    }
}
