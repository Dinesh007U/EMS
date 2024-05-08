using AppUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppUI.Controllers
{
    ///<summary>
    /// -- =================================================================================
    /// -- Author:		SindhuNathan T
    /// -- Create date: 23.04.2024
    /// -- Description:	This controller, named CommonPageController, handles the common layout of this website it will show home page for user
    /// -- =================================================================================
    /// </summary>
    [AllowAnonymous]
    [Route("Common")]
    public class CommonPageController : Controller
    {
        //properties of commonpage controller
        private readonly IConfiguration _configuration;
        private string baseUrl;

        ///<summary>
        /// Constructor for CommonPageController.
        ///</summary>
        ///<param name="configuration">An IConfiguration object injected via dependency injection, providing access to configuration settings.</param>
        public CommonPageController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("WebApiBaseURL");
            baseUrl += "UserInfo";

        }
        /// Action method for rendering the home page.
        [Route("/")]

        [Route("Home", Name = "Home")]// Specifies routes for accessing the event catalogue page.
        public IActionResult HomePage()
        {
            return View();
        }
        /// Action method for rendering the event catalogue page.
        [Route("EventCatalogue", Name = "EventCatalogue")]
        public IActionResult EventCataloguePage()
        {
            return View();
        }

    }
}

