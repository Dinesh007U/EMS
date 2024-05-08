using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Net.Mime;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAny")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionInfoRepo<SessionInfo> sessions;

        public SessionController(ISessionInfoRepo<SessionInfo> sesssion)
        {
            sessions = sesssion;
        }

        /// <summary>
        /// Get all session details
        /// http://localhost:53045/api/session/GetAllSessions
        /// </summary> 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetAllSessions")]
        public IActionResult GetAllSessions()
        {
            if (sessions.GetAllSessionDetails().Count > 0)
            {
                return Ok(sessions.GetAllSessionDetails());
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get session details by session ID
        /// http://localhost:53045/api/session/GetBySessionId/
        /// </summary>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetBySessionId/{id}")]

        public IActionResult GetBySessionId(Guid id)
        {
            var session = sessions.GetSessionById(id);
            return Ok(session);
        }

        /// <summary>
        /// Add a new session
        /// http://localhost:53045/api/session/AddSessionInfo
        /// </summary>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddSessionInfo")]
        [Authorize(Roles = "Admin")]

        public IActionResult AddSessionInfo([FromBody] SessionInfo sessionInfo)
        {
            if (sessionInfo != null)
            {
                sessions.SaveSessionDetails(sessionInfo);
                return CreatedAtAction(nameof(AddSessionInfo), sessionInfo);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update session details
        /// http://localhost:53045/api/session/UpdateSession
        /// </summary>

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("UpdateSession")]
        [Authorize(Roles = "Admin")]

        public IActionResult UpdateSession([FromBody] SessionInfo sessionInfo)
        {

            if (sessionInfo != null)
            {
                var session = sessions.UpdateSessionDetails(sessionInfo);
                return Ok(session);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete session by Session ID
        /// http://localhost:53045/api/session/DeleteSession/
        /// </summary>

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeleteSession/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteSession(Guid id)
        {
            var session = sessions.DeleteSessionDetails(id);
            return Ok(session);
        }

        /// <summary>
        /// Get session details by Event ID
        /// http://localhost:53045/api/session/GetSessionDetailsByEventId/
        /// </summary>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetSessionDetailsByEventId/{id}")]
        public IActionResult GetSessionDetailsByEventId(Guid id)
        {
            var session = sessions.GetEventWithSessions(id);
            return Ok(session);
        }

        /// <summary>
        /// Get all events with their respective session details
        /// http://localhost:53045/api/session/GetAllEventsWithSessionDetails
        /// </summary>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetAllEventsWithSessionDetails")]
        public IActionResult GetAllEventsWithSessionDetails()
        {
            if (sessions.GetAllEventWithSessions().Count > 0)
            {
                return Ok(sessions.GetAllEventWithSessions());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
