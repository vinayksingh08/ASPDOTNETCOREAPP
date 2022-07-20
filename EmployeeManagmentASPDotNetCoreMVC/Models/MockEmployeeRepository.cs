using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

         public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
        {
            new Employee() { ID = 1, Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com" },
            new Employee() { ID = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com" },
            new Employee() { ID = 3, Name = "Sam", Department = Dept.Admin, Email = "sam@pragimtech.com" },
        };
        }

        public Employee AddEmployee(Employee Employee)
        {
            Employee.ID= _employeeList.Max(e => e.ID) + 1;
            _employeeList.Add(Employee);
            return Employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee Emp = _employeeList.FirstOrDefault(e => e.ID== id);
            if (Emp != null)
            {
                _employeeList.Remove(Emp);
            }
            return Emp;

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int ID)
        {
            return _employeeList.FirstOrDefault(e => e.ID == ID);
        }
        public Employee GetEmployee()
        {
            return _employeeList.FirstOrDefault();
        }

        public Employee UpdateEmployee(Employee Employee)
        {
            Employee Emp = _employeeList.FirstOrDefault(e => e.ID == Employee.ID);
            if (Emp != null)
            {
                Emp.Name = Employee.Name;
                Emp.Email = Employee.Email;
                Emp.Department = Employee.Department;
            }
            return Emp;
        }
    }
}
