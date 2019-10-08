using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
   public  interface IEmployeeRepository
    {
        Employee getEmployee(int id);
        IEnumerable<Employee> getAllEmployees();
        Employee Add(Employee e);
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
    }
}
