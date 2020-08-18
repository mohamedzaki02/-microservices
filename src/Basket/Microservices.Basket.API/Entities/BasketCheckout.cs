namespace Microservices.Basket.API.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
    }


}