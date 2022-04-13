using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodWay.Controllers
{
    [Authorize(Roles =Roles.Admin +","+Roles.User)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.Create(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Category category = _categoryRepository.GetById(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.Update(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            Category category = _categoryRepository.GetById(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Delete(int Id) {
            Category category = _categoryRepository.GetById(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int Id, Category category)
        {
            category = _categoryRepository.GetById(Id);
            _categoryRepository.Delete(category);
            return RedirectToAction(nameof(Index));
        }
    }
}