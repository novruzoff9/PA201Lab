using RabbitMQ.Client;

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

int num = 0;
while (true)
{
    num++;
    string message = $"Hello  {num}";
    byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
    await channel.BasicPublishAsync(
    exchange: "",
    routingKey: "test",
    body: body
    );

    Thread.Sleep(1000);
}
