// See https://aka.ms/new-console-template for more information
using App.Core.Helper;
using App.Core.Models;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Hello, Consumer!");


var factory = new ConnectionFactory() { HostName = "localhost" };
using (IConnection connection = factory.CreateConnection())
using (IModel channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "basicMessageQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var chatMessage = RabbitMQHelper.MessageResolve<UserChatMessage>(ea.Body);
        Console.WriteLine($" Received Message: ");
        Console.WriteLine($" Sender Name: {chatMessage.SenderName} Message Content: {chatMessage.Message} ");
        Console.WriteLine($" Time :{chatMessage.Time.ToString("dd MMMM yyyy hh:ss")} ");
        Console.WriteLine(new String('-', 35));
        Console.WriteLine("");
    };
    channel.BasicConsume(queue: "basicMessageQueue",
                         autoAck: true,
                         consumer: consumer);

    Console.ReadKey();
}