using FoodWay.Data.IRepository;
using FoodWay.Data.Models;

namespace FoodWay.Data.Repository
{
    public class CustomerRepository: Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(FoodWayContext context):base(context) { }
    }
}
