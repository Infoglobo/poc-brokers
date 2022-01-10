using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsumerRabbitMq
{
    public static class Native
    {
        private static ConnectionFactory _factory;
        public const string endpoint = "rabbitmq.dev.valorpro.com.br";
        public static string user = "user";
        public static string password = "VmwozUZ7jO";
        public const int port = 30380;        

        public static async void run(string queue)
        {
            
            _factory = new ConnectionFactory
            {
                HostName = endpoint,
                Port = port,
                UserName = user,
                Password = password
            };


            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    //
                };
                Dictionary<string, object> arguments = new Dictionary<string, object>();
                arguments.Add("x-queue-type", "stream");

                channel.QueueDeclare(queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                channel.BasicConsume(queue: queue,
                                     autoAck: true,
                                     consumer: consumer);                
            }
        }
    }
}
