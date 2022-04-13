using FoodWay.Data.IRepository;
using FoodWay.Data.Models;

namespace FoodWay.Data.Repository
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(FoodWayContext context):base(context) { }
    }
}
