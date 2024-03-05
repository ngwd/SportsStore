using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SportsStore.Models;
using SportsStore.Controllers;


namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Pagination()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock
                .Setup(m => m.Products)
                .Returns((new Product[] {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"},
                    new Product {ProductID = 4, Name = "P4"},
                    new Product {ProductID = 5, Name = "P5"}
                })
                .AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>; 

            // assert 
            Product[] prod = result.ToArray();
            Assert.True(2 == prod.Length);
            Assert.Equal("P4", prod[0].Name);
            Assert.Equal("P5", prod[1].Name);
        }
    }
}
