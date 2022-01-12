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
                    BootstrapServers = "10.33.0.68:9092",
                    GroupId = "gValor",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoOffsetStore = false,
                    EnableAutoCommit = true,
                };

                using (var consumer = new ConsumerBuilder<string, string>(config).Build())
                {
                    consumer.Subscribe("first_topic");
                    try
                    {
                        int qtdeMsgs = 0;
                        while (true)
                        {

                            if (qtdeMsgs > 150000)
                            {
                                Console.WriteLine($"Consumed {qtdeMsgs} in {DateTime.Now.ToString("hh.mm.ss.ffffff")}");
                                qtdeMsgs = 0;
                            }

                            var msg = consumer.Consume(TimeSpan.FromSeconds(1));
                            if (msg != null)
                                qtdeMsgs++;
                            //Console.WriteLine($"Consumed {qtdeMsgs}");
                        }
                    }

                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Stopping");
                        consumer.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
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