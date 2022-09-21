using ProductStore.Models.Entities;

namespace ProductStore.Models.ViewModels
{
    public class ProductsIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public int? SelectedProduct { get; set; }
    }
}
