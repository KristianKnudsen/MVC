using ProductStore.Models.Entities;
using ProductStore.Models.ViewModels;

namespace ProductStore.Models.Repos.ProductRepo
{
    public interface IProductRepository
    {
        abstract ICollection<Product> GetProducts();
        abstract void Save(Product product);
        abstract void Update(Product product);
        abstract void Delete(Product product);
        abstract ProductsEditViewModel GetProductEditViewModel();
        abstract ProductsEditViewModel GetProductEditViewModel(int productId);
    }
}
