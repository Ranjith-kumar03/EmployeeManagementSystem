using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    [Route("[Controller]/[action]")]
    [Route("[Controller]")]
    public class homeController:Controller
    {
        private readonly IEmployeeRepository _repos;
        private readonly IHostingEnvironment env;
        private readonly ILogger<homeController> logger;

        public homeController(IEmployeeRepository repos, IHostingEnvironment env, ILogger<homeController> logger)
        {
            _repos = repos;
            this.env = env;
            this.logger = logger;
        }
        [AllowAnonymous]
        [Route("")]
        [Route("~/")]
       
        public ViewResult index()
        {
          return  View(_repos.getAllEmployees());
        }
        [AllowAnonymous]
        [Route("{id?}")]
        public ViewResult Details(int ? id)
        {
            logger.LogTrace("Log Trace");
            logger.LogDebug("Log Debug");
            logger.LogInformation("Log Information");
            logger.LogWarning("Log Warning");
            logger.LogError("Log Error");
            logger.LogCritical("Log Critical");

            //Cheking the employee id for Not availabe id

            Employee emp = _repos.getEmployee(id.Value);
            //Will throw Excption press continue to see the exception Handled
           // throw new Exception("Iam thrown from Details");
            if (emp==null)
            {
                Response.StatusCode = 404;
                return View("EmplooyeeNotFound", id.Value);
            }
            HomeDetailsViewModel hdvm = new HomeDetailsViewModel()
            {
                employee = emp,

                PageTitle = "Employee Details"
            };


            return View(hdvm);
        }
        [HttpGet]
       
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
       
        public IActionResult Create(EmpoloyeeCreateViewModel model)
        {
            string uniqueFileName= processUpLoadedFile(model); ;
         
            if (ModelState.IsValid)
            {
                //We need to Create Employee Object Out of model
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    Photopath = uniqueFileName

                };
                 _repos.Add(newEmployee);
                //Change the Create and Details View for handling photos remove noimage.jpgthere
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        
        public ViewResult Edit(int id)
        {
            
           
            Employee employee = _repos.getEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email= employee.Email,
                Department=employee.Department,
                ExistingPhotoPath=employee.Photopath

            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        
        public IActionResult Edit(EmployeeEditViewModel model)

        {
            Employee upDateEmployee = _repos.getEmployee(model.Id);
            upDateEmployee.Name = model.Name;
            upDateEmployee.Email = model.Email;
            upDateEmployee.Department = model.Department;
            //Checking for photo property not eqal to null for determining changes made to Photos
            if (model.Photo != null)
            {
                if (model.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(env.WebRootPath, "images", model.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                //Updating Photos of Updated Photos if photos changes
                upDateEmployee.Photopath = processUpLoadedFile(model);
            }


            if (ModelState.IsValid)
            {


                _repos.Update(upDateEmployee);

                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }


        //Created a seprate method for Handlig photo file upload used Interface for  EmpoloyeeCreateViewModel
        //as the parent iNterface  so both EmpoloyeeCreateViewModel and EmployeeEditViewModel use the same method
        private string processUpLoadedFile(EmpoloyeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                ///To get the Phyiscal Path of the root folder we use IConfiguration 
                /// need to inject IConfiguration here acessroot folder

                string upLoadsFolder = Path.Combine(env.WebRootPath, "images");
                //We want the file Name to be Unique  so we use GUID class
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                //lets combine upLoadsFolder and  uniqueFileName
                string filePath = Path.Combine(upLoadsFolder, uniqueFileName);
                //Added Usinf since the Filestream to be closed afetr Uploading fle
                using(var fileStream=new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                

            }

            return uniqueFileName;
        }
    }
}
