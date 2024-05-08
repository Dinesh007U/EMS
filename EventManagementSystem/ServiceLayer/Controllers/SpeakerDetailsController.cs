using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAny")]
    public class SpeakerDetailsController : ControllerBase
    {
        private readonly ISpeakerDetailsInfoRepo<SpeakersDetails> _speakerDetails;


        //using dependency injection
        public SpeakerDetailsController(ISpeakerDetailsInfoRepo<SpeakersDetails> speakerDetailsInfoRepo)
        {
            _speakerDetails = speakerDetailsInfoRepo;
        }

        [HttpGet]
        [Route("GetAllSpeakers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        /// <summary>
        /// This action method is used to get all the speaker details
        /// http://localhost:53045/api/speakerdetails/GetAllSpeakers
        /// </summary>

        public IActionResult GetAllSpeakers()
        {
            var getAllspeakers = _speakerDetails.GetAllSpeakerDetails();
            if (_speakerDetails.GetAllSpeakerDetails().Count > 0)
            {
                return Ok(_speakerDetails.GetAllSpeakerDetails());
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// This method is used to save the speaker details
        /// http://localhost:53045/api/speakerdetails/SaveSpeakerDetails
        /// </summary>

        [HttpPost]
        [Route("SaveSpeakerDetails")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]


        public IActionResult PostSpeakerDetails([FromBody] SpeakersDetails speaker)
        {
            if (speaker != null)
            {
                if (_speakerDetails.SaveSpeakerDetails(speaker))
                {
                    return CreatedAtAction(nameof(PostSpeakerDetails), speaker);

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This action method is used to get the speaker details by id
        /// http://localhost:53045/api/speakerdetails/GetSpeakerById/
        /// </summary>

        [HttpGet]
        [Route("GetSpeakerById/{id?}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSpeakerById(string? id)
        {
            var speakerDetails = _speakerDetails.GetSpeakerById(new System.Guid(id));

            return speakerDetails != null ? Accepted(speakerDetails) : NotFound();
        }

        /// <summary>
        /// This method is used to update the speaker details
        /// http://localhost:53045/api/speakerdetails/UpdateSpeakerDetails
        /// </summary>

        [HttpPut]
        [Route("UpdateSpeakerDetails")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]


        public IActionResult PutSpeakerDetails([FromBody] SpeakersDetails details)
        {
            if (details != null)
            {
                var isUpdate = _speakerDetails.UpdateSpeakerDetails(details);
                return isUpdate ? Accepted(details) : NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// This action method is used to delete the  SpeakerDetails
        /// http://localhost:53045/api/speakerdetails/DeleteSpeakerDetails/
        /// </summary>

        [HttpDelete]
        [Route("DeleteSpeakerDetails/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]


        public IActionResult DeleteSpeakerDetails(string? id)
        {
            var details = _speakerDetails.GetSpeakerById(new Guid(id));

            if (details != null)
            {
                _speakerDetails.DeleteSpeakerDetails(new Guid(id));
                return Ok(details);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// This method is used to all the speaker by  passing the event id
        /// http://localhost:53045/api/speakerdetails/GetAllSpeakerByEventId/
        /// </summary>

        [HttpGet]
        [Route("GetAllSpeakerByEventId/{id?}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SpeakersDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetAllSpeakerByEventId(string? id)
        {
            var speakerDetail = _speakerDetails.GetAllSpeakerForEvent(new Guid(id));
            if (speakerDetail.Count != 0)
            {
                return Accepted(speakerDetail);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
