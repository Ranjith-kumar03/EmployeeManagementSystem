using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        
        // GET: /<controller>/
        /// <summary>
        //so statuscode accepts 404 aswell as 500 also
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch(statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry The Resource You Requested Cannot Be Found";
                    //we Use CSharp 6 String Interpolation
                    logger.LogWarning($"404 Error Occured Path={statusCodeResult.OriginalPath} and the Tag Qury is {statusCodeResult.OriginalQueryString} ");
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;
                        break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            //We need to Log Those Exceptions
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //we Use CSharp 6 String Interpolation
            logger.LogError($"The Path{exceptionDetails.Path} threw an Exception {exceptionDetails.Error.Message}");
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            return View("error");
        }
    }
}
