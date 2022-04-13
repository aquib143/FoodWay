using System.ComponentModel.DataAnnotations;

namespace FoodWay.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Unit { get; set; }
        public double Price { get; set; }

    }
}