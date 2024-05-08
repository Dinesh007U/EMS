using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Reflection;

namespace ServiceLayer.Controllers
{
    /// <summary>
    /// Controller for managing user information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAny")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoRepo<UserInfo> _userInfoRepo;

        /// <summary>
        /// Constructor for UserInfoController.
        /// </summary>
        /// <param name="userInfoRepo">The repository for user information.</param>
        public UserInfoController(IUserInfoRepo<UserInfo> userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }

        /// <summary>
        /// Retrieves all user information.
        /// </summary>
        /// http://localhost:53045/api/UserInfo/GetAllUserInfo  
        /// <returns>Returns a list of all user information.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetAllUserInfo")]
        public IActionResult GetAll()
        {
            var allUserInfo = _userInfoRepo.GetAllUserInfo();
            return allUserInfo != null ? Ok(allUserInfo) : NotFound();
        }

        /// <summary>
        /// Retrieves user information by email.
        /// </summary>
        /// http://localhost:53045/api/UserInfo/GetUserInfoByEmail/
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>Returns user information corresponding to the provided email.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetUserInfoByEmail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var getByEmailUserInfo = _userInfoRepo.GetUserInfoByEmail(email);
            return getByEmailUserInfo != null ? Ok(getByEmailUserInfo) : NotFound();
        }

        /// <summary>
        /// Saves user information.
        /// </summary>
        /// http://localhost:53045/api/UserInfo/SaveUserInfo
        /// <param name="userInfo">The user information to save.</param>
        /// <returns>Returns the saved user information.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SaveUserInfo")]
        public IActionResult PostEventoInfo([FromBody] UserInfo userInfo)
        {
            if (userInfo != null)
            {
                _userInfoRepo.SaveUserInfo(userInfo);
                return CreatedAtAction(nameof(GetByEmail), new { email = userInfo.EmailId }, userInfo);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Removes user information by email.
        /// </summary>
        /// http://localhost:53045/api/userinfo/RemoveUserInfo/
        /// <param name="email">The email of the user to remove.</param>
        /// <returns>Returns the removed user information.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("RemoveUserInfo/{email}")]
        public IActionResult DeleteById(string email)
        {
            if (email != null)
            {
                var removeUserInfo = _userInfoRepo.DeleteUserInfo(email);
                return Ok(removeUserInfo);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// http://localhost:53045/api/UserInfo/UpdateUserInfo
        /// <param name="userInfo">The updated user information.</param>
        /// <returns>Returns the updated user information.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("UpdateUserInfo")]
        public IActionResult PutById([FromBody] UserInfo userInfo)
        {
            if (userInfo != null)
            {
                var getByEmail = _userInfoRepo.UpdateUserInfo(userInfo);
                return Accepted(userInfo);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
