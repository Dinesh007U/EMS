using AppUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AppUI.Controllers
{
    ///<summary>
    /// -- =================================================================================
    /// -- Author:		Koushikrajaa M
    /// -- Create date: 23.04.2024
    /// -- Description:	This controller, named AdminController, handles the administrative tasks related to events, speakers, and sessions in the application. It includes methods for managing events, speakers, and sessions such as adding, updating, deleting, and retrieving details. The controller interacts with a web API for data operations. Below is a summary of the functionalities implemented in this controller.
    /// -- =================================================================================
    /// </summary>
    [AllowAnonymous]
    [Route("Admin")]
    public class AdminController : Controller
    {
        //This Method is used to Get All The event details
        [Route("GetAllEvents", Name = "GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            ViewBag.tokenId = HttpContext.Session.GetString("token");

            return View();
        }
        //This Method is used to Show AddEvent page to user
        [HttpGet]
        [Route("AddEvents", Name = "AddEvents")]
        public IActionResult AddEvents()
        {
            ViewBag.tokenId = HttpContext.Session.GetString("token");//Get Jwt token for access the api method

            return View();

        }
        //This Method is used to submit the  AddEvent details to data base
        [HttpPost]
        [Route("AddEvents", Name = "AddEvents")]
        public IActionResult AddEvents(EventDetails eventDetails)
        {
            if (eventDetails != null)
            {
                //Handle Validations
                if (ModelState.IsValid)
                {
                    return View("GetAllEvents");
                }
                else
                {
                    ViewBag.tokenId = HttpContext.Session.GetString("token");

                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        //This Method is used to Search the events
        [Route("SearchEvents", Name = "SearchEvents")]
        public IActionResult SearchEvents()
        {
            ViewBag.tokenId = HttpContext.Session.GetString("token");
            return View();
        }

        //This Method is used to update the event 
        [HttpGet]
        [Route("UpdateEvents/{id}", Name = "UpdateEvents")]
        public IActionResult UpdateEvents(string? id)
        {
            ViewBag.tokenId = HttpContext.Session.GetString("token");

            ViewBag.EventId = id;
            return View();
        }


        //Speaker Related CRUD operations 
        ///<summary>
        /// -- =================================================================================
        /// -- Author:		Santhosh B K
        /// -- Create date: 23.04.2024
        /// -- Description:	This controller, named AdminController, handles the administrative tasks related to events, speakers, and sessions in the application. It includes methods for managing events, speakers, and sessions such as adding, updating, deleting, and retrieving details. The controller interacts with a web API for data operations. Below is a summary of the functionalities implemented in this controller.
        /// -- =================================================================================
        /// </summary>
        private readonly IConfiguration _configuration;
        private string baseUrl;
        public AdminController(IConfiguration configuration)
        {
            this._configuration = configuration;
            baseUrl = _configuration.GetValue<string>("WebApiBaseURL");
        }

        //This Method is used to GetAll The Speaker Details 
        [Route("GetAllSpeakers", Name = "GetAllSpeakers")]
        public async Task<IActionResult> GetAllSpeakers()
        {
            List<SpeakersDetails> events = new List<SpeakersDetails>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}speakerdetails/GetAllSpeakers"))
                {
                    // Check if the Resource will be there or not if it is there return resource
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        events = JsonConvert.DeserializeObject<List<SpeakersDetails>>(apiResponse); //converting json string to List<EventInfo>
                    }
                }
            }
            return View(events);
        }


        //This Method is used to Add the Speaker Details
        [HttpPost]
        [Route("AddSpeaker", Name = "AddSpeaker")]
        public async Task<IActionResult> AddSpeaker(SpeakersDetails speakerInfo)
        {
            var strToken = HttpContext.Session.GetString("token");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(speakerInfo), Encoding.UTF8, "application/json");
                //Passing Tokens
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);//Tokens saved in session

                using (var response = await httpClient.PostAsync($"{baseUrl}speakerdetails/SaveSpeakerDetails", content))
                {
                    // Check if the Add was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllSpeakers");
                    }
                    else
                    {
                        return Content("Error");
                    }
                }
            }
        }


        //This Method is used to Update the Speaker Details
        [Route("UpdateSpeakerDetails", Name = "UpdateSpeakerDetails")]
        public async Task<IActionResult> UpdateSpeakerDetails(string SpeakerName, string SpeakerId)
        {
            SpeakersDetails speakersDetails = new SpeakersDetails { SpeakerId = new System.Guid(SpeakerId), SpeakerName = SpeakerName };
            var strToken = HttpContext.Session.GetString("token");//return jwt token

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(speakersDetails), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

                using (var response = await httpClient.PutAsync($"{baseUrl}speakerdetails/UpdateSpeakerDetails", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //the process will success it returns success status code
                        return RedirectToAction("GetAllSpeakers");
                    }
                    else
                    {
                        //The process will failed it reurn this content
                        return Content("Speaker is not updated");
                    }
                }
            }
        }

        //This Method used to delete the speaker details by using id
        [Route("DeleteSpeaker/{id}", Name = "DeleteSpeaker")]
        public async Task<ActionResult> DeleteSpeaker(string id)
        {
            var strToken = HttpContext.Session.GetString("token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

                using (var response = await httpClient.DeleteAsync($"{baseUrl}speakerdetails/DeleteSpeakerDetails/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle success
                        return RedirectToAction("GetAllSpeakers"); // Return success status
                    }
                    else
                    {
                        // Handle failure
                        return Content("Error"); // Return error status
                    }
                }
            }
        }

        //Session Related CRUD operations 

        //Session
        // Action to show all sessions        
        [HttpGet("GetAllSession/{id}", Name = "GetAllSession")]
        public async Task<IActionResult> GetAllSession(string id)
        {
            

            EventDetails eventDetails = new EventDetails();
            List<SessionInfo> sessions = new List<SessionInfo>();
            List<SpeakersDetails> speakers = new List<SpeakersDetails>();
            Dictionary<SessionInfo, SpeakersDetails> sessionAndSpeakerDetail = new Dictionary<SessionInfo, SpeakersDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}EventDetails/GetEventDetailsById/{id}"))
                {
                    // Process response if needed
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Converting json string to EventDetails
                        eventDetails = JsonConvert.DeserializeObject<EventDetails>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync($"{baseUrl}Session/GetSessionDetailsByEventId/{id}"))
                {
                    // Process response if needed
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Converting json string to List<SessionInfo>
                        sessions = JsonConvert.DeserializeObject<List<SessionInfo>>(apiResponse);
                    }
                }

                using (var response = await httpClient.GetAsync($"{baseUrl}SpeakerDetails/GetAllSpeakerByEventId/{id}"))
                {
                    // Process response if needed
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Converting json string to List<SpeakerInfo>
                        speakers = JsonConvert.DeserializeObject<List<SpeakersDetails>>(apiResponse);
                    }
                }

                foreach (var speaker in sessions)
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}SpeakerDetails/GetSpeakerById/{speaker.SpeakerId}"))
                    {
                        // Process response if needed
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //Converting json string to List<SessionInfo>
                            sessionAndSpeakerDetail.Add(speaker, JsonConvert.DeserializeObject<SpeakersDetails>(apiResponse));
                        }
                    }
                }

            }
            Admin admin = new Admin
            {
                AllEventDetails = eventDetails,
                SessionAndSpeakerDetails = sessionAndSpeakerDetail
            };
            return View(admin);
        }

        // Action to add a session (GET)
        [HttpGet("AddSession/{id}", Name = "AddSession")]
        public async Task<IActionResult> AddSession(string id)
        {
            TempData["eventId"] = id;
            await LoadAllSpeaker();
            return View();
        }

        // Action to add a session (POST)
        [HttpPost("SaveSession", Name = "SaveSession")]
        public async Task<IActionResult> SaveSession(SessionInfo sessionInfo)
        {
            var strToken = HttpContext.Session.GetString("token");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(sessionInfo), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

                using (var response = await httpClient.PostAsync($"{baseUrl}Session/AddSessionInfo", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Process valid sessionInfo
                        // Redirect to action after successful POST, providing the required route parameter 'id'
                        return RedirectToAction("GetAllSession", new { id = sessionInfo.EventId });
                    }
                    else
                    {
                        // ModelState is invalid, return the view with errors
                        return View("AddSession");
                    }
                }
            }

        }

        //This method is used to display all the speaker name in dropDown
        [NonAction]
        public async Task LoadAllSpeaker()
        {
            var speakers = new List<SpeakersDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}SpeakerDetails/GetAllSpeakers"))
                {
                    // Process response if needed
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Converting json string to List<SpeakerInfo>
                        speakers = JsonConvert.DeserializeObject<List<SpeakersDetails>>(apiResponse);
                    }
                }
            }

            var speaker = new List<SelectListItem>();

            foreach (var i in speakers)
            {
                speaker.Add(new SelectListItem { Text = i.SpeakerName.ToString(), Value = i.SpeakerId.ToString() });

            }
            ViewBag.AllSpeakers = speaker;
        }

        //This action method used to Edit session by id 
        [HttpGet]
        [Route("EditSession/{id}", Name = "EditSession")]
        public async Task<IActionResult> EditSession(string id)
        {
            // Create a new SessionInfo object to hold session information
            SessionInfo sessionInfo = new SessionInfo();

            using (var httpClient = new HttpClient())
            {
                // Construct the URL to the API endpoint
                using (var response = await httpClient.GetAsync($"{baseUrl}Session/GetBySessionId/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        // Deserialize the JSON response into a SessionInfo object
                        sessionInfo = JsonConvert.DeserializeObject<SessionInfo>(apiResponse);
                    }
                }
            } 
            // Load all speaker information 
            await LoadAllSpeaker();

            // Return the EditSession view with the retrieved session information
            return View("EditSession", sessionInfo);

        }
        // Action to add a session (Put)
        [HttpPost]
        [Route("UpdateSession", Name = "UpdateSession")]
        public async Task<IActionResult> UpdateSession(SessionInfo sessionInfo)
        {

            // Retrieve the authentication token from the session
            var strToken = HttpContext.Session.GetString("token");

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(sessionInfo), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

                using (var response = await httpClient.PutAsync($"{baseUrl}Session/UpdateSession", content))
                {
                    // Check if the update was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllSession", new { id = sessionInfo.EventId });
                    }
                    else
                    {
                        return Content("Error");
                    }
                }
            }
        }

        //This action method handle the delete functionaly of session
        [Route("DeleteSession/{id}/{eventId}", Name = "DeleteSession")]
        public async Task<IActionResult> DeleteSession(string id, string eventId)
        {
            SessionInfo sessionInfo = new SessionInfo();
            // Check if the update was successful
            var strToken = HttpContext.Session.GetString("token");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

                using (var response = await httpClient.DeleteAsync($"{baseUrl}Session/DeleteSession/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllSession", new { id = eventId });
                    }
                    else
                    {
                        return Content("Error");
                    }
                }
            }
        }

    }
}
