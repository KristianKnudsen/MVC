using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductStore.Controllers;
using ProductStore.Models.Entities;
using ProductStore.Models.Repos.ProductRepo;
using ProductStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProductUnitTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IProductRepository> _repository;
        private ProductController _controller;
        private List<Product> _fakeProducts;

        [TestInitialize]
        public void SetupController()
        {
            _repository = new Mock<IProductRepository>();
            var vm = new ProductsEditViewModel();
            vm.ProductId = 1337;

            _fakeProducts = new List<Product>
            {
                new Product(){Name = "Product1", Description = "AboutProduct1", ManufacturerId = 1, CategoryId = 2},
                new Product(){Name = "Product2", Description = "AboutProduct2", ManufacturerId = 3, CategoryId = 4}
            };
            _repository.Setup(x => x.GetProducts()).Returns(_fakeProducts);
            _controller = new ProductController(_repository.Object);
        }

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            var result = (ViewResult)_controller.Index();
            Assert.IsTrue(result.ViewData.Model is ProductsIndexViewModel);
            Assert.IsNotNull(result, "View Result is null");
            var products = result.ViewData.Model as ProductsIndexViewModel;
            Assert.AreEqual(2, products.Products.Count(), "Got wrong number of products");
        }

        [TestMethod]
        public void PostCreate()
        {
            var vm = new ProductsEditViewModel();
            vm.ProductId = 1337;

            //Seksjon tatt fra https://uit.instructure.com/courses/27044/pages/testing-av-kode-som-benytter-tempdata?module_item_id=664639
            _controller.ControllerContext =
                MockHelpers.FakeControllerContext(false);
            var tempData = new
                TempDataDictionary(_controller.ControllerContext.HttpContext,
                    Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
            //

            var result = _controller.Create(vm);
            Assert.IsTrue(result is RedirectToActionResult);
        }

        [TestMethod]
        public void Edit()
        {
            var vm = new ProductsEditViewModel();
            var id = 5;
            vm.ProductId = id;
            _repository.Setup(x => x.GetProductEditViewModel(id)).Returns(vm);
            var result = _controller.Edit(id);
            Assert.AreEqual((((ViewResult)result).ViewData.Model as ProductsEditViewModel).ProductId, id);
        }

        [TestMethod]
        public void PostEdit()
        {
            var vm = new ProductsEditViewModel();
            vm.ProductId = 4;

            //Seksjon tatt fra https://uit.instructure.com/courses/27044/pages/testing-av-kode-som-benytter-tempdata?module_item_id=664639
            _controller.ControllerContext =
                MockHelpers.FakeControllerContext(false);
            var tempData = new
                TempDataDictionary(_controller.ControllerContext.HttpContext,
                    Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
            //

            var result = _controller.Edit(vm);
            Assert.IsTrue(result is RedirectToActionResult);

        }

        [TestMethod]
        public void PostCreateModelFailes()
        {
            var vm = new ProductsEditViewModel();
            vm.ProductId = 1337;
            vm.Description = "123456789876542123456789";
            _controller.ModelState.AddModelError("test", "test");
            var result = _controller.Create(vm);
            Assert.IsTrue(result is ViewResult);
        }

        [TestMethod]
        public void CreateReturnsViewModel()
        {
            var repo = new Mock<IProductRepository>();
            var vm = new ProductsEditViewModel();
            vm.ProductId = 1337;

            List<Product> fakeProducts = new List<Product>
            {
            };
            repo.Setup(x => x.GetProducts()).Returns(fakeProducts);
            repo.Setup(x => x.GetProductEditViewModel()).Returns(vm);
            var controller = new ProductController(repo.Object);
            var result = ((ViewResult)controller.Create()).ViewData.Model as ProductsEditViewModel;
            Assert.AreSame(vm, result);
            Assert.AreEqual(vm.ProductId, result.ProductId);
        }



    }

    //Tatt fra https://uit.instructure.com/courses/27044/pages/testing-av-kode-som-benytter-tempdata?module_item_id=664639
    public static class MockHelpers
    {
        public static StringBuilder LogMessage = new StringBuilder();
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser
            : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null,
                null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }
        public static ControllerContext FakeControllerContext(bool isLoggedUser =
            true)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim("name", "Ola Nordman"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = isLoggedUser ? user
                    : null
            };
            return new ControllerContext()
            {
                HttpContext = httpContext,
            };
        }
    }
}