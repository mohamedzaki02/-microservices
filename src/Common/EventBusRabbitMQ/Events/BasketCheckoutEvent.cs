using System;

namespace EventBusRabbitMQ.Events
{
    public class BasketCheckoutEvent
    {
        public Guid RequestId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}