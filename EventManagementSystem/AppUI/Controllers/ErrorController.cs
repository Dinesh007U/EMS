using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AppUI.Controllers
{
    public class ErrorController : Controller
    {
        // Action method to handle HTTP status code errors
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            // Retrieve information about the original request
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                //400-499:Client side error
                case 404:
                    // Pass the original requested path, status code, and error message to the view
                    ViewBag.statusCodeResult = statusCodeResult.OriginalPath;
                    ViewBag.statusCode = statusCode;
                    ViewBag.errorMessage = "Sorry, the resource you requested is not found";
                    break;
                //500-599:Server side error
                case 500:
                    // Pass the original requested path, status code, and error message to the view
                    ViewBag.statusCodeResult = statusCodeResult.OriginalPath;
                    ViewBag.statusCode = statusCode;
                    ViewBag.errorMessage = "Sorry,Server Side Error";
                    break;
            }
            return View();
        }
        // Action method to handle general errors
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            // Retrieve information about the exception that occurred
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.executionPath = exceptionDetails.Path;
            ViewBag.errorMessage = exceptionDetails.Error.Message;
            // Return the error view
            return View("Error");
        }
    }
}
