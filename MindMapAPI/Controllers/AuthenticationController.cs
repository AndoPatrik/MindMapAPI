using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MindMapAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace MindMapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        static List<User> users = new List<User>() { new User("Admin", "Pw") };

        // POST: api/Authentication
        [HttpPost]
        public IActionResult AuthenticateUser([FromBody]User user)
        {
            var foundUser = users.Find(x => x.Name == user.Name);

            if (foundUser is null || foundUser.Password != user.Password) 
            {
                return Unauthorized("Incorrect credentials. JWT is not granted.");
            }

            string secretKey = "very_secret_key_which_needs_to_be_hidden";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new List<Claim>();
            //claims.Add(new Claim("Identity", $"{userID}"));
            //claims.Add(new Claim("Role", $"{role}"));

            var token = new JwtSecurityToken(
                    issuer: "mindmap",
                    audience: "users",
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    //claims: claims
                    );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AuthCheck() 
        {
            return Ok("Success.Validated with JWT.");
        }

        [Route("/Register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] User user) 
        {
            if (user.Name == String.Empty || user.Password == String.Empty) 
            {
                return Conflict("Incorrect fields or data to register new user.");
            }

            users.Add(new User(user.Name, user.Password));
            return Ok("User created in the temp list.");
        }

    }
}
