using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 101, Name = "Raju", Email = "guugu@nuju.com", Department = Dept.HR },
                 new Employee() { Id = 501, Name = "Saju", Email = "Auugu@nuju.com", Department = Dept.None },
                  new Employee() { Id = 401, Name = "Taju", Email = "Buugu@nuju.com", Department = Dept.IT },
                  new Employee() { Id = 701, Name = "Uaju", Email = "Cuugu@nuju.com", Department = Dept.PayRoll }


                );
        }

    }
}
