using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RpcClient.Classes
{
    public class Client : IClient
    {

        public Task<IResponseMessage> SendAsync(IRequestMessage requestMessage)
        {
            var rpcClient = new RpcClient();
            Console.WriteLine(" [x] Requesting {0}", requestMessage.text);
            var response = rpcClient.Call(requestMessage.text);
            Console.WriteLine(" [.] Got '{0}'", response);
            Console.WriteLine("----------Press [Enter] to exit---------");
            IResponseMessage responseMessage = new ResponseMessage(response);
            return Task.FromResult(responseMessage);
        }

        public Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage) where TResponseMessage : IResponseMessage
        {
            var rpcClient = new RpcClient();
            Console.WriteLine(" [x] Requesting {0}", requestMessage.text);
            var response = rpcClient.Call(requestMessage.text);
            Console.WriteLine(" [.] Got '{0}'", response);
            Console.WriteLine("----------Press [Enter] to exit---------");
            IResponseMessage responseMessage = new ResponseMessage(response);
            return Task.FromResult((TResponseMessage)responseMessage);
        }
    }
}
