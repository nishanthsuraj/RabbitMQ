using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

const string Url = "amqp://guest:guest@localhost:5672";
const string QueueName = "demo-queue";
SingleConsumer();

void SingleConsumer()
{
    ConnectionFactory factory = new ConnectionFactory { Uri = new Uri(Url) };
    using IConnection connection = factory.CreateConnection();
    using IModel channel = connection.CreateModel();
    channel.QueueDeclare(queue: QueueName, durable: true,
        exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (sender, e) =>
    {
        byte[] body = e.Body.ToArray();
        string message = Encoding.UTF8.GetString(body);
        Console.WriteLine(message);
    };
    channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
    Console.ReadLine();
}
