using ProductStore.Models.Entities;

namespace ProductStore.Models.Repos.ProductRepo
{
    public class FakeProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>{
                new Product {Name="FakeRepo", Price=121.50m, Category="Verktøy"},
                new Product {Name="Vinkelsliper", Price=1520.00m, Category ="Verktøy"},
                new Product {Name="FakeRepo", Price=14.50m, Category ="Dagligvarer"},
                new Product {Name="Kjøttkaker", Price=32.00m, Category ="Dagligvarer"},
                new Product {Name="FakeRepo", Price=25.50m, Category ="Dagligvarer"}
            };
        ICollection<Product> IProductRepository.GetProducts()
        {
            return products;
        }

        public void Save(Product product)
        {
            products.Add(product);
        }
    }
}
