using System;

namespace FoodWay.Data.Models
{
    public class BaseEntity
    {
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
