using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll().ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeRepository.Create(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Employee employee = _employeeRepository.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeRepository.Update(employee);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            Employee employee = _employeeRepository.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Delete(int Id)
        {
            Employee employee = _employeeRepository.GetById(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int Id, Employee employee)
        {
            employee = _employeeRepository.GetById(Id);
            _employeeRepository.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}