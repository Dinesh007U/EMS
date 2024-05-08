using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Cors;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAny")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantEventDetailsRepo<ParticipantEventDetails> _participantEventDetailsRepo;

        // Constructor for the ParticipantController
        public ParticipantController(IParticipantEventDetailsRepo<ParticipantEventDetails> participantEventDetailsRepo)
        {
            _participantEventDetailsRepo = participantEventDetailsRepo;
        }

        /// <summary>
        /// Endpoint to get all participant details
        /// http://localhost:53045/api/participant/GetAllParticipantDetails
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantEventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetAllParticipantDetails")]
        public IActionResult GetAllParticipant()
        {
            // Retrieve all participant details from the repository
            var allParticipantDetails = _participantEventDetailsRepo.GetAllParticipantDetails();
            // Return 200 OK response with participant details if found, otherwise return 404 Not Found
            return allParticipantDetails != null ? Ok(allParticipantDetails) : NotFound();
        }

        /// <summary>
        /// Endpoint to get participant details by ID
        /// http://localhost:53045/api/participant/GetParticipantDetailsById/792471c1-fdd8-4fd4-b47b-adf842f7db5b
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantEventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetParticipantDetailsById/{id}")]
        public IActionResult GetParticipantById(Guid id)
        {
            // Retrieve participant details by ID from the repository
            var participantDetailsById = _participantEventDetailsRepo.GetParticipantById(id);
            // Return 200 OK response with participant details if found, otherwise return 404 Not Found
            return participantDetailsById != null ? Ok(participantDetailsById) : NotFound();
        }

        /// <summary>
        /// Endpoint to save participant event details
        /// http://localhost:53045/api/participant/SaveParticipantEventDetails
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ParticipantEventDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SaveParticipantEventDetails")]
        public IActionResult PostParticipantInfo([FromBody] ParticipantEventDetails participantEventDetails)
        {
            if (participantEventDetails != null)
            {
                // Save participant event details to the repository
                _participantEventDetailsRepo.SaveParticipantDetails(participantEventDetails);
                // Return 201 Created response with the saved participant details
                return CreatedAtAction(nameof(PostParticipantInfo), participantEventDetails);
            }
            else
            {
                // Return 400 Bad Request if participant details are null
                return BadRequest();
            }
        }

        /// <summary>
        /// Endpoint to delete participant details
        /// http://localhost:53045/api/participant/DeleteParticipant/368a404f-6fdd-4bd6-bef3-bc1bbe032bcf
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantEventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeleteParticipant/{id}")]
        public IActionResult DeleteParticipant(Guid id)
        {
            if (id != null)
            {
                // Delete participant details from the repository by ID
                var removeParticipantEventDetails = _participantEventDetailsRepo.DeleteParticipantDetails(id);
                // Return 200 OK response with removed participant details
                return Ok(removeParticipantEventDetails);
            }
            else
            {
                // Return 404 Not Found if ID is null
                return NotFound();
            }
        }

        /// <summary>
        /// Endpoint to update participant details
        /// http://localhost:53045/api/participant/UpdateParticipantDetails
        /// </summary>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ParticipantEventDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("UpdateParticipantDetails")]
        public IActionResult UpdateParticipantDetails([FromBody] ParticipantEventDetails participantEventDetails)
        {
            if (participantEventDetails != null)
            {
                // Update participant details in the repository
                var getParticipant = _participantEventDetailsRepo.UpdateParticipantDetails(participantEventDetails);
                // Return 202 Accepted response with updated participant details
                return Accepted(getParticipant);
            }
            else
            {
                // Return 404 Not Found if participant details are null
                return NotFound();
            }
        }
    }
}
