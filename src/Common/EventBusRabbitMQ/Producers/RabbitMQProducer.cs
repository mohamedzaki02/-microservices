using System;
using System.Text;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;

namespace EventBusRabbitMQ.Producers
{
    public class RabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;
        public RabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent checkoutEvent)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);


                channel.ConfirmSelect();

                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                props.DeliveryMode = 2;

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Basket Checkout Event has been Published Successfully !");
                };

                channel.BasicPublish("", queueName, true, props, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(checkoutEvent)));
                channel.WaitForConfirmsOrDie();

                channel.ConfirmSelect();
            }
        }
    }
}