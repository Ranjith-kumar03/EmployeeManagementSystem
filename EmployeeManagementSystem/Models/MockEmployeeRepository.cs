using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _list;

        public MockEmployeeRepository()
        {
            _list = new List<Employee>() {
                new Employee (){Id=101, Name="Raju",Email="guugu@nuju.com",Department=Dept.HR},
                 new Employee (){Id=501, Name="Saju",Email="Auugu@nuju.com",Department=Dept.None},
                  new Employee (){Id=401, Name="Taju",Email="Buugu@nuju.com",Department=Dept.IT},
                  new Employee (){Id=701, Name="Uaju",Email="Cuugu@nuju.com",Department=Dept.PayRoll},


            };

        }

        public Employee Add(Employee e)
        {
           e.Id= _list.Max(a => a.Id) + 1;
            _list.Add(e);
            return e;
        }

        public Employee Delete(int id)
        {
            Employee employee = _list.FirstOrDefault(a => a.Id == id);
            if(employee!=null)
            {
                _list.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> getAllEmployees()
        {
            return _list;
        }

        public Employee getEmployee(int id)
        {
            return _list.FirstOrDefault((a) => a.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _list.FirstOrDefault(a => a.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
