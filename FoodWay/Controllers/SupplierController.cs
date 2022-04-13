using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            var suppliers = _supplierRepository.GetAll().ToList();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }
            _supplierRepository.Create(supplier);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Supplier supplier = _supplierRepository.GetById(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }
            _supplierRepository.Update(supplier);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            Supplier supplier = _supplierRepository.GetById(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        public IActionResult Delete(int Id)
        {
            Supplier supplier = _supplierRepository.GetById(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Delete(int Id, Supplier supplier)
        {
            supplier = _supplierRepository.GetById(Id);
            _supplierRepository.Delete(supplier);
            return RedirectToAction(nameof(Index));
        }

    }
}