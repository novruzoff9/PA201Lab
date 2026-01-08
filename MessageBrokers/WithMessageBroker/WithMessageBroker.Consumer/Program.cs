using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory connectionFactory = new()
{
    Uri = new Uri("amqps://swfkgzrt:kJcPP4FVQFS88BsWo74YoE1drX8_efmQ@cow.rmq2.cloudamqp.com/swfkgzrt")
};

using IConnection connection = await connectionFactory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "test",
    exclusive: false,
    autoDelete: false
);

AsyncEventingBasicConsumer consumer = new(channel);

await channel.BasicConsumeAsync(
            queue: "test",
            autoAck: true,
            consumer: consumer);

consumer.ReceivedAsync += async (sender, ev) =>
{
    byte[] body = ev.Body.ToArray();
    string message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($"Alinan mesaj: {message}");
};

Console.Read();