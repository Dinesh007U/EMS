using DAL.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using DAL.Models;
using System.Linq;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserInfoRepo<UserInfo> _userDetailsRepo;

        //Using the dependency injection through constructor
        public LoginController(IConfiguration config, IUserInfoRepo<UserInfo> userInfo)
        {
            this._config = config;
            _userDetailsRepo = userInfo;
        }

        //This method is used to redirect to validate and generate token method

        [HttpPost]
        [Route("Auth")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Login([FromBody] UserInfo userLogin)
        {
            var user = Authentication(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }


        //This method is used to Generate the token
        [HttpGet]
        [NonAction]
        private string GenerateToken(UserInfo userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userLogin.UserName),
                new Claim(ClaimTypes.Role,userLogin.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //This method is used to validate the admin credentials
        [HttpGet]
        [NonAction]
        private UserInfo Authentication(UserInfo userLogin)
        {

            if (userLogin != null)
            {
                var currentUser = _userDetailsRepo.GetAllUserInfo().Where(x => x.EmailId == userLogin.EmailId && x.Password == userLogin.Password && x.Role == "Admin").FirstOrDefault();

                return currentUser != null ? currentUser : null;
            }
            return null;

        }
    }
}
