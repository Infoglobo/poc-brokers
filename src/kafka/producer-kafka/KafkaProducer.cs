using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace kafka
{

    public class KafkaProducer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var producer = new ProducerBuilder<Null, string>(config).Build())
                    {
                        var result = await producer.ProduceAsync("cotacoes", new Message<Null, string> { Value = "a log message" });
                    }
                }
            }
            catch (Exception)
            {
                Dispose();
            }
        }
    }
}