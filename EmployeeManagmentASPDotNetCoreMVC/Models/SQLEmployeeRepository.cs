using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        public AppDbContext Context { get; }
        public SQLEmployeeRepository(AppDbContext context)
        {
            Context = context;
        }

      

        public Employee AddEmployee(Employee Employee)
        {
            Context.Employees.Add(Employee);
            Context.SaveChanges();
            return Employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp = Context.Employees.Find(id);
            if (emp!=null)
            {

                Context.Employees.Remove(emp);
                Context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return Context.Employees;
        }

        public Employee GetEmployee(int ID)
        {
            return Context.Employees.Find(ID);
        }

        public Employee UpdateEmployee(Employee Employee)
        {
            var emp = Context.Employees.Attach(Employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return Employee;
        }
    }
}
