using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Utilities
{
    public class ValidEmailAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailAttribute(string AllowedDomain)
        {
            allowedDomain = AllowedDomain;
        }
        public override bool IsValid(object value)
        {
           string[] strings= value.ToString().Split('@');
           return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
