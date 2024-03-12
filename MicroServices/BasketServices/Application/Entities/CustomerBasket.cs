using Microservices.Application.Entities;

namespace BasketServices.Application.Entities
{
    public class CustomerBasket
    {
        public string CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
        public CustomerBasket() { 
            Items = new List<BasketItem>();
        }
    }
}
