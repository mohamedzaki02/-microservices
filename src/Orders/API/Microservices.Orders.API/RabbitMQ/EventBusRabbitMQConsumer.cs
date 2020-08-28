using System;
using System.Text;
using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Microservices.Orders.API.Profiles;
using Microservices.Orders.Application.Commands;
using Microservices.Orders.Core.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservices.Orders.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        private readonly IOrderRepository _repo;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IOrderRepository repo)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(EventBusConstants.BasketCheckoutQueue, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);

            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);
                var result = await _mediator.Send(_mapper.Map<BasketCheckoutCommand>(basketCheckoutEvent));
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}