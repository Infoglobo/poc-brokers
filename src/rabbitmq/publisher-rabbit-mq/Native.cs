using Newtonsoft.Json;
using PublisherRabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace publisher_rabbit_mq
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
            {
                using (var channel = connection.CreateModel())
                {
                    var message = new Message();
                    var stringMessage = JsonConvert.SerializeObject(message);
                    var byteMessage = Encoding.UTF8.GetBytes(stringMessage);

                    while (true)
                    {
                        for (var i = 0; i <= 10000; i++)
                        {
                            channel.BasicPublish(
                                exchange: "exchange1",
                                routingKey: "cotacoes",
                                basicProperties: null,
                                body: byteMessage
                            );
                        }
                        Thread.Sleep(1000);
                        
                    }
                }
            }
        }

    }
}
