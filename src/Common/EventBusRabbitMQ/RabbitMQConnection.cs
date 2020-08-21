using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _factory;
        private IConnection _connection;
        private bool _disposed;
        public RabbitMQConnection(IConnectionFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            if (!IsConnected) TryConnect();

        }
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected) throw new InvalidOperationException("No RabbitMQ Connection");
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _connection.Dispose();
            _disposed = true;
        }

        public bool TryConnect()
        {
            try
            {
                _connection = _factory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(2000);
                _connection = _factory.CreateConnection();
            }

            if (IsConnected) return true;
            return false;
        }
    }
}