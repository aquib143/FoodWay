using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodWay.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FoodWayContext context) : base(context) { }

        public override IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).Include(p => p.Supplier);
        }

        public override Product GetById(int Id)
        {
           return _context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefault(x=>x.Id == Id);
        }
    }
}
