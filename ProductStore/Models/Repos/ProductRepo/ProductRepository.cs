using ProductStore.Data;
using ProductStore.Models.Entities;

namespace ProductStore.Models.Repos.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository( ApplicationDbContext db )
        {
            this._db = db;
        }

        ICollection<Product> IProductRepository.GetProducts()
        {
            return _db.Product.ToList<Product>();
        }

        void IProductRepository.Save(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
