using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using AppUI.Models;

namespace AppUI.Controllers
{
    [AllowAnonymous]
    [Route("Login")]

    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private string baseUrl;

        //Using dependenct injection and also setting the baseUrl from the configuration file
        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.baseUrl = this._configuration.GetValue<string>("WebAPIBaseUrl");
            this.baseUrl += "Login";//http://localhost:44370/api/Login


        }

        //This mthod is used for showing admin view page
        [Route("AdminLoginPage", Name = "AdminLoginPage")]
        public IActionResult AdminLoginPage()
        {
            try
            {
                string email = Request.Cookies["AdminEmail"].ToString();
                string password = Request.Cookies["AdminPassword"].ToString();
                ViewBag.AdminEmail = email;
                ViewBag.AdminPassword = password;
            }
            catch
            {

            }

            return View();
        }


        //This method is used for the validate Admin 
        //This method is used to call the webApi for Generate the token
        [HttpPost]
        [Route("ValidateAdmin", Name = "ValidateAdmin")]
        public async Task<IActionResult> ValidateAdmin(string email, string password, string saveCredentials)
        {

            UserInfo user = new UserInfo { EmailId = email, Password = password, Role = "Admin", UserName = "Defaultuser", IsDeleted = false };

            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/Auth", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (saveCredentials == "remember")
                        {
                            CookieOptions cookieOptions = new CookieOptions();
                            cookieOptions.Path = "/";
                            cookieOptions.Secure = true;
                            cookieOptions.Expires = DateTime.Now.AddMinutes(20);
                            cookieOptions.Domain = "localhost";
                            Response.Cookies.Append("AdminEmail", email, cookieOptions);
                            Response.Cookies.Append("AdminPassword", password, cookieOptions);


                        }
                        //Storing tokens in web sessions
                        string token = await response.Content.ReadAsStringAsync();
                        HttpContext.Session.SetString("token", token);
                        return RedirectToRoute(new
                        {
                            controller = "Admin",
                            action = "GetAllEvents"
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Enter the valid Credentials!");
                        return View("AdminLoginPage");
                    }
                }
            }
        }

    }
}
