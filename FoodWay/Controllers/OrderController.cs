using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Data.ViewModels;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class OrderController : Controller
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IShipperRepository _shipperRepository;

        [BindProperty]
        public OrderViewModel OrderVM { get; set; }

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository, IShipperRepository shipperRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _shipperRepository = shipperRepository;

            OrderVM = new OrderViewModel
            {
                Customers = _customerRepository.GetAll(),
                Employees = _employeeRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
        }
        
        public IActionResult Index()
        {
            //if (!_repository.Any()) return View("Empty");

            List<OrderViewModel> orderVM = new List<OrderViewModel>();

            var orders = _orderRepository.GetAll().ToList();

            foreach (var order in orders)
            {
                orderVM.Add(new OrderViewModel
                {
                    Order = order,
                    OrderCount = _orderDetailRepository.Find(x => x.OrderId == order.Id).Count()
                });
            }

            return View(orderVM);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            
            var order = _orderRepository.GetById((int)Id);
            if (order == null)
            {
                return View("Empty");
            }

            var masterDetailViewModel = new MasterDetailViewModel
            {
                Order = _orderRepository.GetById((int)Id),
                Products = _productRepository.GetAll().ToList()
            };
            return View(masterDetailViewModel);
        }

        public IActionResult Create() {
            return View(OrderVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(OrderVM);
            }
            _orderRepository.Create(OrderVM.Order);

            TempData["Success"] = "The data has been added...!";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int Id)
        {
            OrderVM.Order = _orderRepository.GetById(Id);
            if (OrderVM.Order == null)
            {
                return NotFound();

            }
            return View(OrderVM);
        }

        //Action for Edit Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Update(OrderVM.Order);

                TempData["Success"] = "The data has been updated...!";

                return RedirectToAction("Index");
            }
            return View();
        }

        //Delete
        public IActionResult Delete(int Id)
        {
            OrderVM.Order = _orderRepository.GetById(Id);
            if (OrderVM.Order == null)
            {
                return NotFound();

            }
            return View(OrderVM);
        }

        //Delete
        [HttpPost]
        public ActionResult Delete(int Id, Order order)
        {
            order = _orderRepository.GetById(Id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                _orderRepository.Delete(order);
            }
            return RedirectToAction("Index");
        }

        //Master Details for OrderDetail and Order

        public IActionResult MasterDetails(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            //var order = _context.Orders.Include(o => o.OrderDetails).Include(o => o.Customer)
            //    .FirstOrDefault(m => m.Id == id);
            var order = _orderRepository.GetById((int)Id);
            if (order == null)
            {
                return View("Empty");
            }
            var masterDetailViewModel = new MasterDetailViewModel
            {
                //Order = _context.Orders.Include(o => o.OrderDetails).Include(o => o.Shipper).Include(o => o.Employee).Include(o => o.Customer)
                //.FirstOrDefault(m => m.Id == id),
                Order= _orderRepository.GetById((int)Id),
                Products = _productRepository.GetAll().ToList()
            };
            return View(masterDetailViewModel);
        }
    }
}