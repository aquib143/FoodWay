using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWay.Data.Repository
{
    public class OrderDetailRepository: Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(FoodWayContext context):base(context){}
    }
}
