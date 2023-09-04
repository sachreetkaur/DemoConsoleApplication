using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleAppReciever
{
    public class MessageReciever : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        public MessageReciever(IModel channel)
        {
            _channel = channel;
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
                try
                {
                    byte[] bodyBytes = body.ToArray();
                    var message = Encoding.UTF8.GetString(bodyBytes);
                   Console.WriteLine("Received message: " + message);

                _channel.BasicPublish(exchange: "", routingKey: "reminderConsumedQueue", basicProperties: null, body: body);

                    _channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing message: " + ex.Message);
                    _channel.BasicNack(deliveryTag: deliveryTag, multiple: false, requeue: true);
                }
        }
    }
}
