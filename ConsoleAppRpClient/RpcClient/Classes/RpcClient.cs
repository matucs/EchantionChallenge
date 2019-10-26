using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace RpcClient.Classes
{

    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly AsyncEventingBasicConsumer consumer;
        private readonly IBasicProperties props;
        private readonly string correlationId;
        private string response;
        private AutoResetEvent waitHandle  = new AutoResetEvent(false);

        public delegate void ResultHandler(object model, BasicDeliverEventArgs ea);


        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            factory.DispatchConsumersAsync = true;

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new AsyncEventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += Consumer_Received;
        }


        private Task<string> Consumer_Received(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            response = Encoding.UTF8.GetString(body);
            Console.WriteLine("CorrelationId --> " + correlationId);
            Console.WriteLine("Message Get  --> " + response);
            waitHandle.Set();
            return Task.FromResult<string>(response);
        }
        public static void OnResult(object sender, BasicDeliverEventArgs args)
        {
            Console.WriteLine();
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
            waitHandle.WaitOne();
            return response;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
