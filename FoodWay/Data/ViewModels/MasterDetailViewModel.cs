using FoodWay.Data.Models;
using System.Collections.Generic;

namespace FoodWay.Data.ViewModels
{
    public class MasterDetailViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
