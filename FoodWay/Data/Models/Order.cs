using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodWay.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string OrderNumber { get; set; }

        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Ordered Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Shipper")]
        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }


        [InverseProperty("Order")]
        public List<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        [Display(Name = "Order No")]
        public string OrderNo
        {
            get
            {
                return OrderNumber;
            }
        }
    }
}
