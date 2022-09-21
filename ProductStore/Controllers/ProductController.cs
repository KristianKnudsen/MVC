using Microsoft.AspNetCore.Mvc;
using ProductStore.Models.Entities;
using ProductStore.Models.Repos.ProductRepo;
using ProductStore.Models.ViewModels;

namespace ProductStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly List<Product> _products;
        private readonly IProductRepository repo;
        public ProductController(IProductRepository repo)
        {
            this.repo = repo;
            _products = repo.GetProducts().ToList();
        }
        public IActionResult Index()
        {
            var pivm = new ProductsIndexViewModel();
            pivm.Products = _products;
            return View(pivm);
        }

        public ActionResult Create()
        {
            var product = repo.GetProductEditViewModel();
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var product = repo.GetProductEditViewModel(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit([Bind("Name,Description,Price,ManufacturerId,CategoryId, ProductId")]
            ProductsEditViewModel product)
        {
            var pId = new ProductsIndexViewModel() { SelectedProduct = product.ProductId };
            Delete(pId);
            return Create(product);
        }

        [HttpPost]
        public ActionResult Delete([Bind("SelectedProduct")] ProductsIndexViewModel pivm)
        {
            var id = pivm.SelectedProduct;
            if (id != null || id != 0)
            {
                var product = new Product
                {
                    ProductId = (int)id
                };
                repo.Delete(product);
            }

            return RedirectToAction("Index");
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create([Bind("Name,Description,Price,ManufacturerId,CategoryId")] 
            ProductsEditViewModel product)
        {
            try
            {
                if ( !ModelState.IsValid)
                {
                    throw new InvalidDataException("Invalid input data for method" );
                }
                Product newProduct = new Product();
                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                newProduct.Price = product.Price;
                newProduct.ManufacturerId = product.ManufacturerId;
                newProduct.CategoryId = product.CategoryId;
                repo.Save(newProduct);
                TempData["message"] = string.Format("{0} har blitt opprettet", product.Name);
                return RedirectToAction("Index");
            } 
            catch
            {
                return View(repo.GetProductEditViewModel());
            }
        }
    }
}
