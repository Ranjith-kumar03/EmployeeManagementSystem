using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Employee employee { get; set; }
        public string PageTitle { get; set; }
    }
}
