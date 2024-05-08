using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;



namespace WebApi.Controllers
{
    /// <summary>
    /// Controller for managing event details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly IEventDetailsRepo<EventDetails> _eventDetailsRepo;

        /// <summary>
        /// Constructor for EventDetailsController.
        /// </summary>
        /// <param name="eventDetailsRepo">The repository for event details.</param>
        public EventDetailsController(IEventDetailsRepo<EventDetails> eventDetailsRepo)
        {
            _eventDetailsRepo = eventDetailsRepo;
        }

        /// <summary>
        /// Retrieves all event details.
        /// </summary>
        /// https://localhost:44395/api/EventDetails/GetAllEventDetails
        /// <returns>Returns a list of all event details.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetAllEventDetails")]
        public IActionResult GetAll()
        {
            var allEventDetails = _eventDetailsRepo.GetAllEventDetails();
            return allEventDetails != null ? Ok(allEventDetails) : NotFound();
        }

        /// <summary>
        /// Retrieves event details by ID.
        /// </summary>
        /// https://localhost:44395/api/EventDetails/GetEventDetailsById/
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>Returns event details corresponding to the provided ID.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetEventDetailsById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var getByEmailEventDetails = _eventDetailsRepo.GetEventByEventId(id);
            return getByEmailEventDetails != null ? Ok(getByEmailEventDetails) : NotFound();
        }

        /// <summary>
        /// Saves event details.
        /// </summary>
        /// http://localhost:53045/api/EventDetails/SaveEventDetails
        /// <param name="eventDetails">The event details to save.</param>
        /// <returns>Returns the saved event details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SaveEventDetails")]
        [Authorize(Roles = "Admin")]

        public IActionResult PostEventInfo([FromBody] EventDetails eventDetails)
        {
            if (eventDetails != null)
            {
                _eventDetailsRepo.SaveEventDetails(eventDetails);
                return CreatedAtAction(nameof(PostEventInfo), eventDetails);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Removes event details by ID.
        /// </summary>
        /// http://localhost:53045/api/EventDetails/RemoveEventDetails/
        /// <param name="id">The ID of the event to remove.</param>
        /// <returns>Returns the removed event details.</returns>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("RemoveEventDetails/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteById(Guid id)
        {
            if (id != null)
            {
                var removeEventDetails = _eventDetailsRepo.DeleteEventDetails(id);
                return Ok(removeEventDetails);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates event details.
        /// </summary>
        /// http://localhost:53045/api/EventDetails/UpdateEventDetails
        /// <param name="eventDetails">The updated event details.</param>
        /// <returns>Returns the updated event details.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("UpdateEventDetails")]
        [Authorize(Roles = "Admin")]

        public IActionResult PutById([FromBody] EventDetails eventDetails)
        {
            if (eventDetails != null)
            {
                var getByEmail = _eventDetailsRepo.UpdateEventDetails(eventDetails);
                return Accepted(eventDetails);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetParticipantRegisteredEvents/{email}")]
        public IActionResult GetParticipantRegisteredEvents(string? email)
        {
            var registeredEvents = _eventDetailsRepo.ParticipantRegisteredEvent(email);

            return Ok(registeredEvents);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetEventDetailsByName/{eventName}")]
        public IActionResult GetByEventName(string eventName)
        {
            var getByName = _eventDetailsRepo.GetEventByEventName(eventName);
            return getByName != null ? Ok(getByName) : NotFound();
        }
    }
}

