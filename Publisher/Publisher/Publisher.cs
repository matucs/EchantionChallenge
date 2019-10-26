using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Echantion.Classes.Server;
using System.IO;

class RPCServer
{
    private static IConnection connection;
    private static IModel channel;
    private static EventingBasicConsumer consumer;

    public static void Main()
    {
        Connect();
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

    }
    static void Connect()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: "rpc_queue", durable: false,
          exclusive: false, autoDelete: true, arguments: null);
       // channel.BasicQos(0, 1, false);
        consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume(queue: "rpc_queue",
          autoAck: false, consumer: consumer);
        Console.WriteLine(" [x] Awaiting RPC requests");

        consumer.Received += (model, ea) =>
        {
            string response = null;

            var body = ea.Body;
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [.] {0}", message);
                response = Response(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(" [.] " + e.Message);
                response = "";
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                  basicProperties: replyProps, body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag,
                  multiple: false);
                BasicGetResult result = channel.BasicGet("rpc_queue", false);
                if (result == null)
                {
                    Console.WriteLine("No message available at this time.");
                }
                Console.WriteLine(" [--] {0}", response);
            }
        };
    }
    /// 

    private static string Response(string request)
    {
        Factory factory = new Factory();
        IResponse response = factory.Build(request);
        if (request == "Bye")
        {
            channel.BasicCancel(consumer.ConsumerTag);
           

        }
        return response.MakeResponse();
    }
}