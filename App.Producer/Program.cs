// See https://aka.ms/new-console-template for more information
using App.Core.Helper;
using App.Core.Models;

using RabbitMQ.Client;

Console.WriteLine("Hello, Producer!");

do
{
    Console.Write("Sender Name: ");
    var userName = Console.ReadLine();
    Console.Write("Message Text: ");
    var message = Console.ReadLine();
    UserChatMessage chatMessage = new() { Message = message, SenderName = userName, Time = DateTimeOffset.Now };
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (IConnection connection = factory.CreateConnection())
    using (IModel channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "basicMessageQueue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.BasicPublish(exchange: "",
                             routingKey: "basicMessageQueue",
                             basicProperties: null,
                             body: RabbitMQHelper.MessageToBytes(chatMessage));
        Console.WriteLine($"Message Send.");
    }
} while (true);
