using System.Linq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace kafka
{

    public class KafkaConsumer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "10.10.251.159:9092",
                    GroupId = "gCotacoes",

                    EnableAutoCommit = true, // (the default)
                    EnableAutoOffsetStore = false
                };

                using (var consumer = new ConsumerBuilder<string, string>(config).Build())
                {
                    consumer.Subscribe("cotacoes");
                    try
                    {
                        int qtdeMsgs = 0;
                        while (true)
                        {
                            if (qtdeMsgs > 50000)
                            {
                                Console.WriteLine($"Consumed {qtdeMsgs} in {DateTime.Now.ToString("hh.mm.ss.ffffff")}");

                                qtdeMsgs = 0;
                            }
                            var msg = consumer.Consume(stoppingToken);
                            qtdeMsgs++;
                        }
                        //stopWatch.Stop();
                    }
                    catch (OperationCanceledException)
                    {
                        //exception might have occurred since Ctrl-C was pressed.
                    }
                    finally
                    {
                        // Ensure the consumer leaves the group cleanly and final offsets are committed.
                        consumer.Close();
                    }
                }

            }
            finally
            {
                Dispose();
            }
        }
    }
}