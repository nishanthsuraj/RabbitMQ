using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

const string Url = "amqp://guest:guest@localhost:5672";
const string QueueName = "demo-queue";
SingleProducer();

// Single Producer
void SingleProducer()
{
    ConnectionFactory factory = new ConnectionFactory { Uri = new Uri(Url) };
    using IConnection connection = factory.CreateConnection();
    using IModel channel = connection.CreateModel();
    channel.QueueDeclare(queue: QueueName, durable: true,
        exclusive: false, autoDelete: false, arguments: null);
    (string Name, string Message) Message = (Name: "Producer", Message: "Hello!");
    byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Message));

    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
}
