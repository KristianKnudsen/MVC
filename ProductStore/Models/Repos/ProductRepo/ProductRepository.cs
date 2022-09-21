using Microsoft.EntityFrameworkCore;
using ProductStore.Data;
using ProductStore.Models.Entities;
using ProductStore.Models.ViewModels;

namespace ProductStore.Models.Repos.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public ProductsEditViewModel GetProductEditViewModel()
        {
            ProductsEditViewModel pevm = new ProductsEditViewModel();

            pevm.Manufacturers = _db.Manufacturers.ToList();
            pevm.Categories = _db.Categories.ToList();

            return pevm;
        }

        public ProductsEditViewModel GetProductEditViewModel(int productId)
        {
            ProductsEditViewModel pevm = new ProductsEditViewModel();

            var pd = _db.Products.Find(productId);

            pevm.ProductId = productId;
            pevm.CategoryId = pd.CategoryId;
            pevm.ManufacturerId = pd.ManufacturerId;
            pevm.Description = pd.Description;
            pevm.Price = pd.Price;
            pevm.Name = pd.Name;
            pevm.Manufacturers = _db.Manufacturers.ToList();
            pevm.Categories = _db.Categories.ToList();

            return pevm;
        }

        ICollection<Product> IProductRepository.GetProducts()
        {
            var products = _db.Products
                .Include(cat => cat.Category)
                .Include(man => man.Manufacturer)
                .ToList();

            return products;
        }

        void IProductRepository.Save(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        void IProductRepository.Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }

        void IProductRepository.Delete(Product product)
        {
            var pd = _db.Products.Find(product.ProductId);
            _db.Products.Remove(pd);
            _db.SaveChanges();
        }
    }
}
