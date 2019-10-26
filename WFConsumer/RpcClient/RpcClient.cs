using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RpcClient
{
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly string replyQueueName;
    private readonly AsyncEventingBasicConsumer consumer;
    private readonly ConcurrentQueue<string> respQueue = new ConcurrentQueue<string>();
    private readonly IBasicProperties props;
    private string result;

    public RpcClient()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        replyQueueName = channel.QueueDeclare().QueueName;
        consumer = new AsyncEventingBasicConsumer(channel);

        props = channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueueName;

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body;
            var response = Encoding.UTF8.GetString(body);
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                respQueue.Enqueue(response);
                Console.WriteLine("Delivery: " + ea.DeliveryTag);
                await Task.Yield();
            }
        };
    }

    public string Call(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(
            exchange: "",
            routingKey: "rpc_queue",
            basicProperties: props,
            body: messageBytes);

        channel.BasicConsume(
            consumer: consumer,
            queue: replyQueueName,
            autoAck: true);

        respQueue.TryDequeue(out result);
        return result;
    }

    public void Close()
    {
        connection.Close();
    }
}