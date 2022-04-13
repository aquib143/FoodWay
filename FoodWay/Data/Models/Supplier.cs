using System.ComponentModel.DataAnnotations;

namespace FoodWay.Data.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
