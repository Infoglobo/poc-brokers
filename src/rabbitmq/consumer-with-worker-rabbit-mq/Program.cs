using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerConsumoRabbitMQ
{
    public class Program
    {
        public const string endpoint = "rabbitmq.dev.valorpro.com.br";
        public static string user = String.Empty;
        public static string password = String.Empty;
        public const int port = 30380;
        public static string queue = String.Empty;

        public static void Main(string[] args)
        {
            LoadEnviroments();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ParametrosExecucao>(
                    new ParametrosExecucao()
                    {
                        ConnectionString = $"amqp://{user}:{password}@{endpoint}:{port}",
                        Queue = queue
                    });
                services.AddHostedService<Worker>();
                });
        

        public static void LoadEnviroments()
        {
            queue = Environment.GetEnvironmentVariable("QUEUE");
            user = Environment.GetEnvironmentVariable("RABBIT_USER");
            password = Environment.GetEnvironmentVariable("RABBIT_PASSWORD");
        }
    }
}