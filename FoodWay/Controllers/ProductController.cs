using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Data.ViewModels;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodWay.Controllers
{
    //[Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        [BindProperty]
        public ProductViewModel ProductVM { get; set; }

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            ProductVM = new ProductViewModel()
            {
                Categories = _categoryRepository.GetAll(),
                Suppliers = _supplierRepository.GetAll(),
                Product = new Product()
            };
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create() {
            return View(ProductVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ProductVM);
            }
            _productRepository.Create(ProductVM.Product);

            TempData["Success"] = "The data has been added...!";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            ProductVM.Product = _productRepository.GetById(id);
            if (ProductVM.Product == null)
            {
                return NotFound();

            }
            return View(ProductVM);
        }

        //Action for Edit Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(ProductVM.Product);

                TempData["Success"] = "The data has been updated...!";

                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int Id)
        {
            ProductVM.Product = _productRepository.GetById(Id);
            if (ProductVM.Product == null)
            {
                return NotFound();
            }
            return View(ProductVM);
        }

        //Delete
        public IActionResult Delete(int Id)
        {
            ProductVM.Product = _productRepository.GetById(Id);
            if (ProductVM.Product == null)
            {
                return NotFound();

            }
            return View(ProductVM);
        }

        //Delete
        [HttpPost]
        public ActionResult Delete(int Id, Product product)
        {
            product = _productRepository.GetById(Id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _productRepository.Delete(product);
            }
            return RedirectToAction("Index");
        }
    }
}