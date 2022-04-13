using FoodWay.Data.IRepository;
using FoodWay.Data.Models;

namespace FoodWay.Data.Repository
{
    public class ShipperRepository: Repository<Shipper>, IShipperRepository
    {
        public ShipperRepository(FoodWayContext context):base(context) { }
    }
}
