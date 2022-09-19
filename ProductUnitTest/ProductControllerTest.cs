using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductStore.Controllers;
using ProductStore.Models.Entities;
using ProductStore.Models.Repos.ProductRepo;
using System.Collections;

namespace ProductUnitTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IProductRepository> _repository;

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            // Arrange 

            _repository = new Mock<IProductRepository>();

            List<Product> fakeProducts = new List<Product>{
  new Product {Name="Hammer", Price=121.50m, Category="Verktøy"},
      new Product {Name="Vinkelsliper", Price=1520.00m, Category ="Verktøy"},
      new Product {Name="Melk", Price=14.50m, Category ="Dagligvarer"},
      new Product {Name="Kjøttkaker", Price=32.00m, Category ="Dagligvarer"},
      new Product {Name="Brød", Price=25.50m, Category ="Dagligvarer"}
};

            _repository.Setup(x => x.GetProducts()).Returns(fakeProducts);


            var controller = new ProductController(_repository.Object);

            // Act 
            var result = (ViewResult)controller.Index();

            // Assert 
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model,
typeof(Product));
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as List<Product>;
            Assert.AreEqual(5, products.Count, "Got wrong number of products");

        }
    }
}