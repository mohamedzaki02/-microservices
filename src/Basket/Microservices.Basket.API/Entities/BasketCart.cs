using System.Collections.Generic;

namespace Microservices.Basket.API.Entities
{
    public class BasketCart
    {
        public BasketCart() { }
        public BasketCart(string username)
        {
            this.UserName = username;
        }
        public string UserName { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal _total = 0;
                this.BasketItems.ForEach(item => _total += item.Price * item.Quantity);
                return _total;
            }
        }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}