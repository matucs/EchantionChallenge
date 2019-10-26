namespace RpcClient.Classes
{
    public class RequestMessage : IRequestMessage
    {
        public RequestMessage(string text)
        {
            this.text = text;
        }
        public string text { get; set; }
    }
}