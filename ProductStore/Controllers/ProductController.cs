using Microsoft.AspNetCore.Mvc;
using ProductStore.Models.Entities;
using ProductStore.Models.Repos.ProductRepo;

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
            return View(_products);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                repo.Save(product);
                return RedirectToAction("Index");
            } 
            catch
            {
                return View(_products);
            }
        }
    }
}
