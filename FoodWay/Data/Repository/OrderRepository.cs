using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodWay.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(FoodWayContext context) : base(context) { }

        public override IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Shipper).Include(o => o.Customer).Include(o => o.Employee);
        }

        public override Order GetById(int Id)
        {
            return _context.Orders.Include(o => o.OrderDetails).Include(o => o.Shipper).Include(o => o.Employee).Include(o => o.Customer).FirstOrDefault(m => m.Id == Id);
        }
    }
}
