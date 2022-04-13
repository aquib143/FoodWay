using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodWay.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }

        [NotMapped]
        public string Name
        {
            get { 
            return FirstName + " " + LastName;
            }
        }
    }
}