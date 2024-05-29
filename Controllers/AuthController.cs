using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;
using Programming_Web_API.DTO;
using Programming_Web_API.Repository;
using Programming_Web_API.Helper;


namespace Programming_Web_API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        //Old code for Googles OAuth...

        //[HttpGet("login")]
        //public IActionResult Login()
        //{
        //    var authenticationProperties = new AuthenticationProperties
        //    {
        //        RedirectUri = "/auth/callback"
        //    };
        //    return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet("callback")]
        //public async Task<IActionResult> Callback()
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    if (!authenticateResult.Succeeded)
        //    {
        //        return Redirect("/auth/login");
        //    }

        //    // Process the user info here (e.g., create or update user in the database)

        //    return Redirect("/home");
        //}

        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userRepository.GetUserByEmail(loginDto.Email);


            //If user does not exist
            if (user == null) return BadRequest(new { message = "Invalid Credentials" });


            //If password is not correct
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.ID); //The userID is placed in the Token

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
            });

            //For some reason the cookie is only being set in swagger, not in vue
            //So i have to send the cookie data and create the cookie in vue.js
            return Ok(jwt);

            //return Ok(new
            //{
            //    message = "success"
            //});


        }

        //[HttpGet("user")]
        [HttpGet("user")]
        public IActionResult User(string jwt_value)
        {
            try
            {
                //var jwt = Request.Cookies["jwt"]; //Once again, this only works in swagger...
                //So we have to send the jwt data as a variable in the User() function
                var jwt = jwt_value;

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUser(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
