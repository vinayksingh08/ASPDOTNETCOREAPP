using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Models
{
   public interface IEmployeeRepository
    {
        Employee GetEmployee(int ID);
        IEnumerable<Employee> GetAllEmployee();
        Employee AddEmployee(Employee Employee);
        Employee UpdateEmployee(Employee Employee);
        Employee DeleteEmployee(int id);
    }
}
