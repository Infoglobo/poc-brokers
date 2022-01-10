using MassTransit;
using Newtonsoft.Json;
using PublisherRabbitMq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerRabbitMq
{
    class Program
    {

        public const string endpoint = "rabbitmq.dev.valorpro.com.br";
        public static string user = "user";
        public static string password = "VmwozUZ7jO";
        public const int port = 30380;
        public static string queue = String.Empty;

        public static async Task Main(string[] args)
        {
            LoadEnviroments();
            //Thread tProcess = new Thread(new ThreadStart(ThreadBus));
            Thread tProcess = new Thread(new ThreadStart(ThreadNative));
            Thread tAlive = new Thread(new ThreadStart(Alive));
            tProcess.Start();
            tAlive.Start();
        }

        public static async void ThreadBus() {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                LoadEnviroments();

                cfg.Host(new Uri($"amqp://{user}:{password}@{endpoint}:{port}"));

                cfg.ReceiveEndpoint(queue, e =>
                {
                    e.Consumer<ValueConsumer>();
                });
                cfg.SingleActiveConsumer = true;
            });

            await bus.StartAsync();
        }

        public static async void ThreadNative()
        {
            Native.run(queue);
        }

        public static async void Alive()
        {
            while (true)
            {
                Thread.Sleep(10000);
            }
        }



        public class ValueConsumer : IConsumer<Message>
        {
            public Task Consume(ConsumeContext<Message> context)
            {               
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(context.Message.Value);
                return Task.CompletedTask;
            }
        }     
        
        public static void LoadEnviroments()
        {
            queue = Environment.GetEnvironmentVariable("QUEUE");            
        }
    }
}
