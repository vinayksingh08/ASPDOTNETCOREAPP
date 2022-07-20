using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Name can not exceed 50 Char")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",ErrorMessage ="Invalid Email")]
        [Display(Name="Offial Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
