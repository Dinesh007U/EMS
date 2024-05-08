using AppUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;

namespace AppUI.Controllers
{
    ///<summary>
    /// -- =================================================================================
    /// -- Author:		Dinesh R
    /// -- Create date: 23.04.2024
    /// -- Description:	This controller, ParticipantController, handles the participant Login operations
    /// -- =================================================================================
    /// </summary>
    [Route("ParticipantLogin")]
    public class ParticipantLoginController : Controller
    {
        [Route("AzureLogin", Name = "AzureLogin")]
        public IActionResult AzureLogin()
        {

            // Get user information from Azure Active Directory
            var getUser = GetUserOnAzureAd(HttpContext.User);

            // Store user information in session variables
            HttpContext.Session.SetString("UserName", getUser.Username);
            HttpContext.Session.SetString("email", getUser.UserEmail);
            HttpContext.Session.SetString("UserDomain", getUser.UserDomain);

            // Redirect to the CheckUser action of the Participant controller

            return RedirectToRoute(new { controller = "Participant", action = "CheckUser" });
        }
        // Method to retrieve user information from Azure Active Directory
        public static UserAzureAd GetUserOnAzureAd(ClaimsPrincipal user)
        {
            var preferredUsernameClaim = user.Claims.FirstOrDefault(c => c.Type.Equals("preferred_username"));
            if (preferredUsernameClaim != null)
            {
                // Extract username, email, and domain from claims
                return new UserAzureAd
                {
                    Username = user.Claims.FirstOrDefault(p => p.Type.Equals("name")).Value,
                    UserEmail = preferredUsernameClaim.Value,
                    UserDomain = string.Format(@"dineshruram73gmail.onmicrosoft.com\{0}", preferredUsernameClaim.Value.Split('@')[0])
                };
            }
            return null; // Or throw an exception if preferred_username claim is required
        }
    }
}
