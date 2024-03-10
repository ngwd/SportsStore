using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SportsStore.Models;
using SportsStore.Controllers;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            // IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>;
            ProductListViewModel result = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            // assert 
            Product[] prod = result.Products.ToArray();
            Assert.True(2 == prod.Length);
            Assert.Equal("P4", prod[0].Name);
            Assert.Equal("P5", prod[1].Name);
        }
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3};

            // Act
            ProductListViewModel result = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository> ();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"},
            }).AsQueryable<Product>());
            
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, ProductListViewModel> GetModel = result => result?.ViewData?.Model as ProductListViewModel;

            // Action 
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;


            // assert 
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
