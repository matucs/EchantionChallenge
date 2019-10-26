namespace RpcClient.Classes
{
    public class ResponseMessage:IResponseMessage
    {
       public ResponseMessage(string text)
        {
            this.text = text;
        }
        public string text { get; set; }
    }
}