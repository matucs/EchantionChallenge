using Echantion.Models;
using RpcClient.Classes;
using System;

public class Rpc
{
    public static void Main()
    {    
        while(true)
        {
            IClient client = new Client();
            MessageViewModel request = new MessageViewModel();
            Console.WriteLine("Enter your Message...");
            request.Text = Console.ReadLine();
            request.Response = client.SendAsync(new RequestMessage(request.Text)).Result.text;
        }
    }
}
