using FoodWay.Data.Models;
using System.Collections.Generic;

namespace FoodWay.Data.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public int OrderCount { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Shipper> Shippers { get; set; }
    }
}
