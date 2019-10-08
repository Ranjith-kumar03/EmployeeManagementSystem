using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        //To be Able to Coonect to the Sql Server using Entity frame work we nee dto use DbContext
        //So we inject AppDbContext class  in to here via the Constructor
        public SQLEmployeeRepository(AppDbContext context, ILogger<SQLEmployeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Employee Add(Employee e)
        {
            context.Employees.Add(e);
            context.SaveChanges();
            return e;
        }

        public Employee Delete(int id)
        {
            Employee employee=context.Employees.Find(id);
            if(employee!=null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> getAllEmployees()
        {
            return context.Employees;
        }

        public Employee getEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            logger.LogTrace("Log Trace");
            logger.LogDebug("Log Debug");
            logger.LogInformation("Log Information");
            logger.LogWarning("Log Warning");
            logger.LogError("Log Error");
            logger.LogCritical("Log Critical");
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            //Implementation of Update method Slightly different 
           var employee= context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
