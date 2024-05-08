using AppUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppUI.Controllers
{
    ///<summary>
    /// -- =================================================================================
    /// -- Author:		Dinesh R
    /// -- Create date: 23.04.2024
    /// -- Description:	This controller, ParticipantController, handles the participant operations
    /// -- =================================================================================
    /// </summary>
    [Route("Participant")]
    public class ParticipantController : Controller
    {
        private readonly IConfiguration _configuration;
        private string baseUrl;
        //This is the Constructor of Participant Controller class
        public ParticipantController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("WebApiBaseURL");

        }
        //This is the default participant page
        [Route("ParticipantHomePage", Name = "ParticipantHomePage")]
        public IActionResult ParticipantHomePage()
        {
            ViewBag.ParticipantUserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        //This method is used to redirect to the user to the see the all events

        [Route("ParticipantEventCatalogue", Name = "ParticipantEventCatalogue")]
        public IActionResult ParticipantEventCatalogue()
        {
            ViewBag.ParticipantUserName = HttpContext.Session.GetString("UserName");

            return View();
        }

        //This method is used to redirect to the registration page for the event
        [Route("RegisterEvent/{id?}", Name = "RegisterEvent")]
        public async Task<IActionResult> RegisterEvent(string? id)
        {
            string emailid = HttpContext.Session.GetString("email");
            ViewBag.Emailid = emailid;
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.ParticipantUserName = HttpContext.Session.GetString("UserName");
            var events = new EventDetails();
            var sessions = new List<SessionInfo>();
            var speakers = new List<SpeakersDetails>();
            var isRegistered = false;
            var attended = false;

            // Fetch event, session, and speaker details
            // Using HttpClient to make HTTP requests to the API
            using (var httpClient = new HttpClient())
            {
                // Fetching event details by ID
                using (var response = await httpClient.GetAsync($"{baseUrl}EventDetails/GetEventDetailsById/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var webApiResponse = await response.Content.ReadAsStringAsync();

                        events = JsonConvert.DeserializeObject<EventDetails>(webApiResponse);
                    }
                }
                // Using HttpClient to make HTTP requests to the API
                using (var response = await httpClient.GetAsync($"{baseUrl}Session/GetSessionDetailsByEventId/{id}"))
                {
                    // Fetching session details by event ID
                    if (response.IsSuccessStatusCode)
                    {
                        var webApiResponse = await response.Content.ReadAsStringAsync();

                        sessions = JsonConvert.DeserializeObject<List<SessionInfo>>(webApiResponse);
                    }
                }
                // Using HttpClient to make HTTP requests to the API
                using (var response = await httpClient.GetAsync($"{baseUrl}SpeakerDetails/GetAllSpeakerByEventId/{id}"))
                {
                    // Fetching speaker details by event ID
                    if (response.IsSuccessStatusCode)
                    {
                        var webApiResponse = await response.Content.ReadAsStringAsync();

                        speakers = JsonConvert.DeserializeObject<List<SpeakersDetails>>(webApiResponse);
                    }
                }
                // Check if the participant is registered for the event
                if (emailid.Length > 0)
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}EventDetails/GetParticipantRegisteredEvents/{emailid}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Reading the response content as a string
                            var webApiResponse = await response.Content.ReadAsStringAsync();
                            // Deserializing the JSON response to a list of EventDetails objects
                            var listOfEvents = JsonConvert.DeserializeObject<List<EventDetails>>(webApiResponse);

                            var registeredOrnot = listOfEvents.Where(x => x.EventId.ToString() == id).Any();

                            if (registeredOrnot)
                            {
                                isRegistered = true;
                            }
                        }
                    }

                    using (var response = await httpClient.GetAsync($"{baseUrl}Participant/GetAllParticipantDetails"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Reading the response content as a string
                            var webApiResponse = await response.Content.ReadAsStringAsync();
                            // Deserializing the JSON response to a list of participantDetails objects
                            var allParticipant = JsonConvert.DeserializeObject<List<ParticipantEventDetails>>(webApiResponse);

                            var participantDetail = allParticipant.Where(x => x.ParticipantEmailId == emailid && x.EventId.ToString() == id).FirstOrDefault();


                            if (participantDetail != null)
                            {
                                attended = participantDetail.IsAttended;
                            }
                        }
                    }
                }

            }
            //Creating the object for EventWithSessionDetails
            EventsWithSessionDetails details = new EventsWithSessionDetails
            {
                EventInformations = events,
                SessionDetails = sessions,
                SpeakersInformations = speakers,
                isRegister = isRegistered,
                isAttended = attended


            };

            return View(details);
        }

        //This method is used to display the participant registered events
        [Route("ParticipantRegisteredEvents", Name = "ParticipantRegisteredEvents")]

        public IActionResult ParticipantRegisteredEvents()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.ParticipantUserName = HttpContext.Session.GetString("UserName");
            return View();
        }


        //This method is used for registering the new event 
        [Route("RegisteringNewEvent/{id?}/{email}", Name = "RegisteringNewEvent")]
        public async Task<IActionResult> RegisteringNewEvent(string? email, string? id)
        {
            ParticipantEventDetails participant = new ParticipantEventDetails
            {
                EventId = new Guid(id),
                ID = Guid.NewGuid(),
                IsAttended = false,
                IsDeleted = false,
                ParticipantEmailId = email,

            };
            // Using HttpClient to make HTTP POST request to the API
            using (var httpClient = new HttpClient())
            {
                // Serializing participant object to JSON string and creating content with UTF-8 encoding
                StringContent content = new StringContent(JsonConvert.SerializeObject(participant), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{baseUrl}Participant/SaveParticipantEventDetails", content))
                {
                    // Checking if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ParticipantRegisteredEvents");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }

        }
        //This method is used to logout

        [Authorize]
        [Route("LogOut", Name = "LogOut")]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return RedirectToRoute(new { Controller = "CommonPage", Action = "HomePage" });
        }

        //This method is used to check user is already there or not

        [Route("CheckUser", Name = "CheckUser")]
        public async Task<IActionResult> CheckUser()
        {

            // Retrieve current user's email and username from session
            var currentUserEmail = HttpContext.Session.GetString("email");
            var currentUserName = HttpContext.Session.GetString("UserName");

            // Using HttpClient to make HTTP requests to the API
            using (var httpClient = new HttpClient())
            {
                // Sending GET request to fetch all user information
                using (var response = await httpClient.GetAsync($"{baseUrl}UserInfo/GetAllUserInfo"))
                {
                    // Checking if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        var webApiResponse = await response.Content.ReadAsStringAsync();

                        var users = JsonConvert.DeserializeObject<List<UserInfo>>(webApiResponse);

                        var isExist = users.Where(x => x.EmailId == currentUserEmail).Any();

                        if (isExist)
                        {
                            // If the user exists, redirect to the participant's home page

                            return RedirectToAction("ParticipantHomePage");
                        }
                        else
                        {
                            // If the user exists, redirect to the participant's home page

                            UserInfo userInfo = new UserInfo { EmailId = currentUserEmail, UserName = currentUserName, IsDeleted = false, Password = "NaN1234", Role = "Participant" };

                            StringContent content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

                            using (var responseForPost = await httpClient.PostAsync($"{baseUrl}UserInfo/SaveUserInfo", content))
                            {
                                if (responseForPost.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("ParticipantHomePage");
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        //This method is used to modify the isattended field in participant table
        [Route("isAttendedModify/{email?}/{id?}", Name = "isAttendedModify")]

        public async Task<IActionResult> isAttendedModify(string? email, string? id)
        {

            // Using HttpClient to make HTTP requests to the API
            using (var httpClient = new HttpClient())
            {
                // Sending GET request to fetch all participant details
                using (var response = await httpClient.GetAsync($"{baseUrl}Participant/GetAllParticipantDetails"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var webApiResponse = await response.Content.ReadAsStringAsync();
                        // Deserializing JSON response to a list of ParticipantEventDetails objects
                        var allParticipant = JsonConvert.DeserializeObject<List<ParticipantEventDetails>>(webApiResponse);
                        // Finding participant's details for the specified email and event ID
                        var participantDetail = allParticipant.Where(x => x.ParticipantEmailId == email && x.EventId.ToString() == id).FirstOrDefault();

                        // If participant's details found
                        if (participantDetail != null)
                        {
                            // Updating the isAttended field to true
                            participantDetail.IsAttended = true;
                            StringContent content = new StringContent(JsonConvert.SerializeObject(participantDetail), Encoding.UTF8, "application/json");

                            using (var responseForPut = await httpClient.PutAsync($"{baseUrl}Participant/UpdateParticipantDetails", content))
                            {
                                // If successful, redirect to the event registration page
                                if (responseForPut.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("RegisterEvent", new { id = id });
                                }
                                else
                                {
                                    // If not successful, throw an exception
                                    throw new Exception();
                                }
                            }
                        }
                    }


                }
            }
            throw new Exception();

        }
    }
}
