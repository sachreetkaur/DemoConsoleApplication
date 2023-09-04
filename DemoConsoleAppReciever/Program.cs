using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoConsoleAppReciever;
using RabbitMQ.Client;

namespace DemoConsoleAppReceiver
{
    public class Program
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";
        static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicQos(0, 1, false);
            MessageReciever messageReceiver = new MessageReciever(channel);
            channel.BasicConsume("reminderAddQueue", false, messageReceiver);
            Console.ReadLine();
        }
    }
}
