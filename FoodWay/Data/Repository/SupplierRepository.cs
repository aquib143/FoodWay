using FoodWay.Data.IRepository;
using FoodWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWay.Data.Repository
{
    public class SupplierRepository: Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(FoodWayContext context):base(context) { }
     
    }
}
