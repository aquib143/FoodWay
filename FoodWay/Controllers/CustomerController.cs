using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            var customers = _customerRepository.GetAll().ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Create(customer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Customer customer = _customerRepository.GetById(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            _customerRepository.Update(customer);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            Customer customer = _customerRepository.GetById(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult Delete(int Id)
        {
            Customer customer = _customerRepository.GetById(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(int Id, Customer customer)
        {
            customer = _customerRepository.GetById(Id);
            _customerRepository.Delete(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}