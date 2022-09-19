using ProductStore.Models.Entities;

namespace ProductStore.Models.Repos.ProductRepo
{
    public interface IProductRepository
    {
        abstract ICollection<Product> GetProducts();
        abstract void Save(Product product);
    }
}
