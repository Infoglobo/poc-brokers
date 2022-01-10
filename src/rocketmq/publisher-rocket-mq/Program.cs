using NewLife.RocketMQ;
using NewLife.RocketMQ.Protocol;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace consumerRocketMQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                //Get configuration
                Producer producer = new Producer
                {
                    Topic = "TopicTest",
                    NameServerAddress = "10.33.0.43:9876",
                    Group = "Group"
                };
                
                var message = new Message();
                var msg = new Msg();
                msg.MessageBody = "teste";
                var json = JsonConvert.SerializeObject(msg);

                message.Tags = "tag";                
                message.Keys = "teste";
                message.BodyString = "teste";
                                
                //Start connection
                producer.Start();
                //release the news
                while (true)
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        producer.Publish(message);
                    }
                    Thread.Sleep(1000);
                }
                

                Console.WriteLine(JsonConvert.SerializeObject(message));
                //Release connection
                producer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Write a message queue is wrong:" + ex.ToString());
            }                        
        }
    }
}
