using MassTransit;
using publisher_rabbit_mq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublisherRabbitMq
{
    class Program
    {
        public const string endpoint = "rabbitmq.dev.valorpro.com.br";
        public static string user = String.Empty;
        public static string password = String.Empty;
        public const int port = 30380;
        public static string queue = String.Empty;
        public static int qtdeLote = 10000;

        public static async Task Main(string[] args)
        {
            Thread tProcess = new Thread(new ThreadStart(ThreadNative));
            Thread tAlive = new Thread(new ThreadStart(Alive));
            tProcess.Start();
            tAlive.Start();

            
        }

        public static async void ThreadBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri($"amqp://{endpoint}:{port}"), h =>
                {
                    h.Username(user);
                    h.Password(password);
                    h.PublisherConfirmation = false;
                });
            });
            EndpointConvention.Map<Message>(new Uri($"queue:{queue}"));
            bus.GetSendEndpoint(new Uri($"queue:{queue}"));

            await bus.StartAsync();
            for (int i = 0; i < qtdeLote; i++)
            {
                await Task.WhenAll(Enumerable.Range(0, qtdeLote).Select(i =>
                {
                    var message = new Message { Value = GetMessageToPublish() };
                    return bus.Send(message);
                }));

                Thread.Sleep(1000);
            }            
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


        public static string GetMessageToPublish()
        { 
            string message = "{\"15\":\"BRL\",\"55\":\"ITUB4\",\"63\":0,\"75\":\"2021-11-30T00:00:00Z\",\"107\":\"ITAUUNIBANCOPN EJ N1\",\"167\":6,\"225\":\"2021-11-22T00:00:00Z\",\"231\":1,\"272\":\"2021-11-30T00:00:00Z\",\"273\":\"2021-11-30T18:41:21.218Z\",\"274\":4,\"288\":\"40\",\"289\":\"40\",\"311\":\"ITUB4\",\"326\":3,\"451\":-0.49,\"455\":\"BRITUBACNPR1\",\"461\":\"EPNNPR\",\"470\":\"BR\",\"561\":100,\"762\":21,\"873\":\"2018-01-31T00:00:00\",\"969\":0.01,\"1003\":\"411680\",\"1148\":20.41,\"1149\":24.95,\"1151\":\"71\",\"1306\":1,\"5151\":2,\"6937\":\"ITUB\",\"6939\":4,\"7687\":-2.16,\"9748\":48458450,\"9749\":100,\"37012\":1,\"100001\":22.17,\"100002\":22.75,\"100003\":22.05,\"100004\":22.68,\"100005\":22.17,\"100009\":\"77\",\"100013\":\"Itaú Unibanco Holding S.A., Ação PN, Bovespa\",\"100014\":\"Itaú Unibanco Holding S.A.\",\"100018\":\"Intradiária\",\"100020\":15,\"100021\":22.66,\"100023\":22.66,\"100024\":41155,\"100025\":22.375,\"100027\":23.35,\"100031\":30901300,\"100032\":\"40\",\"100033\":\"16\",\"100034\":100,\"100035\":200,\"100036\":\"2021-11-19T00:00:00Z\",\"100038\":691108435,\"100039\":23.327,\"100040\":\"42662770.000000000\",\"100041\":17767700,\"100042\":22.17,\"100043\":23.4,\"100044\":17.95,\"100045\":23.4,\"100052\":1,\"100071\":0.09,\"100072\":-0.61,\"100073\":0.4,\"100074\":-2.69,\"100087\":33.44,\"100088\":21.88,\"100089\":\"2021-01-08T00:00:00Z\",\"100090\":\"2021-11-22T00:00:00Z\",\"100091\":30.046891585,\"100092\":20.52,\"100093\":\"2018-01-29T10:08:00.207\",\"100094\":\"2020-03-24T10:12:28.717\",\"100095\":31.810847788,\"100096\":31.47669056,\"100097\":44.077591508,\"100098\":0,\"100100\":11.315,\"100102\":2,\"100103\":\"1.697674418604651\",\"100151\":22.18,\"100152\":200,\"100163\":2,\"100164\":9055476,\"100166\":2,\"100167\":-180,\"100168\":\"2021-11-25T21:07:39.815Z\",\"100170\":1,\"100172\":1,\"100174\":\"2021-11-29T00:00:00Z\",\"100176\":22.22,\"100194\":\"Bolsa de Valores de São Paulo\",\"100195\":\"Bovespa\",\"100196\":true,\"100197\":\"Tempo Real\",\"100198\":\"2009-05-20T00:00:00\",\"100199\":\"América do Sul\",\"100207\":\"Itaú Unibanco Holding S.A., Ação PN, Bovespa\",\"100214\":1,\"100215\":\"Brasil\",\"100218\":\"ITUB\",\"100220\":\"http: //www.bmfbovespa.com.br\",\"100222\":11,\"100266\":\"1.077106139933365\",\"100268\":\"53.88095238095238\",\"100276\":-33.98637138,\"100283\":31.014024456,\"100284\":39.675118401,\"100285\":47.233155702,\"100301\":true,\"100304\":11808000,\"100305\":0.42,\"100324\":\"ITAU UNIBANCO PN\",\"100325\":6,\"100326\":4434,\"100327\":13.33,\"100329\":21.01,\"100335\":\"11.786458333333332\",\"100336\":\"1.698948948948948\",\"100337\":\"1.55106237148732\",\"100339\":\"0.105143335036937\",\"100345\":14.59,\"100346\":13.32,\"100347\":1.92,\"100348\":215.23,\"100350\":\"Intermediários Financeiros\",\"100353\":18900,\"100354\":52300,\"100357\":\"2021-11-30T00:00:00Z\",\"100358\":22.17,\"100359\":\"Itaú Unibanco Holding S.A.\",\"100361\":\"R$\",\"100367\":0,\"100383\":1,\"100386\":\"2021-11-30T00:00:00Z\",\"100387\":\"IBOV=5.480080200000000;IBRA=4.622656100000000;IBXL=6.204733100000000;IBXX=5.006995100000000;ICO2=8.399495900000000;IDIV=2.262054900000000;IFNC=16.814758100000000;IGCT=4.890584500000000;IGCX=2.793697800000000;ISEE=4.116375800000000;ITAG=4.921880600000000;MIBV=0.000000000000000;MLCX=5.290012700000000;UD:ITUBVO960000=1.000000000000000;UD:ITUBVO960004=1.000000000000000\",\"100388\":1.18430081,\"100389\":1.19889405,\"900013\":\"2021-11-30T18:41:00Z\",\"900014\":22.17,\"900015\":22.18,\"900016\":22.18,\"900017\":22.17,\"900018\":20,\"900019\":2200,\"900020\":48788,\"900021\":\"2021-11-30T00:00:00Z\"}";
            return message;            
        }
        
        public static void LoadEnviroments()
        {
            queue = Environment.GetEnvironmentVariable("QUEUE");            
        }
    }
}
