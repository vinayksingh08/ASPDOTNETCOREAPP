using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagmentASPDotNetCoreMVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("/Error/{statusCode}")]
        public IActionResult HttpErrorCodeHandler(int statusCode)
        {
            var StatusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, Resources you requested ,Not Found";
                    //ViewBag.path = StatusCodeResult.OriginalPath;
                    //ViewBag.QS = StatusCodeResult.OriginalQueryString;
                    logger.LogWarning($"404 Error Occured. Path {StatusCodeResult.OriginalPath} and Query String {StatusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var ExceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ErrorMessage = ExceptionDetails.Error.Message;
            //ViewBag.path = ExceptionDetails.Path;
            //ViewBag.StackTrace = ExceptionDetails.Error.StackTrace;

            logger.LogError($"The Path {ExceptionDetails.Path} threw an exception {ExceptionDetails.Error}");

            return View("Error");
        }
    }
}
