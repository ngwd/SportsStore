using System.Collections.Generic;
using System.Linq;
namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly IQueryable<Product> _products = new List<Product>
        {
            new Product {Name = "Footbal", Price = 25 },
            new Product {Name = "Surf Board", Price = 179 },
            new Product {Name = "Running Shoes", Price = 95 },
        }.AsQueryable<Product>();
        public IQueryable<Product> Products => _products;
    }
}
