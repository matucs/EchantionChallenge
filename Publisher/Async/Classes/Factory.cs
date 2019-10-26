using Echantion.Classes.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Echantion.Classes.Server
{
    public class Factory
    {
        private Creator creator;
        public IResponse Build(string request)
        {
            try
            {
                switch (request)
                {
                    case "Hello":
                        creator = new CreatorHello();
                        break;
                    case null:
                        creator = new CreatorNull();
                        break;
                    case "Bye":
                        creator = new CreatorBye();
                        break;
                    case "Ping":
                        creator = new CreatorPing();
                        break;
                    default:
                        creator = new CreatorException();
                        break;
                }
                return creator.Instance();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }

        }
    }
}
