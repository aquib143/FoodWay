using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class ShipperController : Controller
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public IActionResult Index()
        {
            var shippers = _shipperRepository.GetAll();
            return View(shippers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Shipper shipper)
        {
            if (!ModelState.IsValid)
            {
                return View(shipper);
            }
            _shipperRepository.Create(shipper);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Shipper shipper = _shipperRepository.GetById(Id);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Shipper shipper)
        {
            if (!ModelState.IsValid)
            {
                return View(shipper);
            }
            _shipperRepository.Update(shipper);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int Id)
        {
            Shipper shipper = _shipperRepository.GetById(Id);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        public IActionResult Delete(int Id)
        {
            Shipper shipper = _shipperRepository.GetById(Id);
            if (shipper == null)
            {
                return NotFound();
            }
            return View(shipper);
        }

        [HttpPost]
        public IActionResult Delete(int Id, Shipper shipper)
        {
            shipper = _shipperRepository.GetById(Id);
            _shipperRepository.Delete(shipper);
            return RedirectToAction(nameof(Index));
        }
    }
}