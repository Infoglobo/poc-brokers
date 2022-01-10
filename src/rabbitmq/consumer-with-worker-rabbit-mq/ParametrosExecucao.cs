namespace WorkerConsumoRabbitMQ
{
    public class ParametrosExecucao
    {
        // Exemplo de Connection String do RabbitMQ:
        // amqp://testes:RabbitMQ2020!@localhost:5672/
        public string ConnectionString { get; set; }
        public string Queue { get; set; }
    }
}