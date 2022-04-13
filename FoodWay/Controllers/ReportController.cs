using AspNetCore.Reporting;
using FoodWay.Data;
using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using FoodWay.Data.ViewModels;
using FoodWay.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace FoodWay.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    public class ReportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly FoodWayContext _context;

        public ReportController(FoodWayContext context, IHostingEnvironment hostingEnvironment, IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
            this._orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            //Encoder.RegisterProvider(CodePagesEncoderProvider.Instance);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._hostingEnvironment.WebRootPath}\\Report\\Order.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("RP1", "RDLC Report Test");

            //Get Product from product table
            var orders = _orderRepository.GetAll();
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dtsOrders", orders);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/Pdf");
        }

        public IActionResult Print1()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._hostingEnvironment.WebRootPath}\\Report\\Order.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("RP1", "RDLC Report Test");

            //Get Product from product table
            var orderVM = new OrderViewModel {
                Orders = _orderRepository.GetAll(),
                Customers = _customerRepository.GetAll()
            };
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", orderVM);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/Pdf");
        }

        public IActionResult repProduct()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._hostingEnvironment.WebRootPath}\\Report\\repProduct.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("RP1", "RDLC Report Test");

            //Get Product from product table
            var products = _context.Products.FromSql("SELECT Products.Id,Products.CategoryId,Products.SupplierId,Products.ProductName, Products.Unit, Products.Price, Categories.Name, Suppliers.Name AS supName FROM Products INNER JOIN Categories ON Products.Id = Categories.Id INNER JOIN Suppliers ON Products.Id = Suppliers.Id");
            //IEnumerable<Product> products = _context.Products.Include(p => p.Category.Name).Include(p => p.Supplier.Name);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dtsProductList", products);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/Pdf");
        }

    }
}
