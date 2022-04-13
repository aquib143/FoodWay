using System.ComponentModel.DataAnnotations;

namespace FoodWay.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "user Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
