using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MindMapAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using MindMapAPI.Util;
using MongoDB.Bson;

namespace MindMapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //TODO: Dependecy Injection
        private MongoConnection mongo = new MongoConnection("MindMapDb");

        static List<TestUser> users = new List<TestUser>() { new TestUser("Admin", "Pw") };

        // POST: api/Authentication
        [HttpPost]
        public IActionResult AuthenticateUser([FromBody]TestUser inputUser)
        {
            var mongoUser = mongo.LoadUserByName(inputUser.Name);
            var hashedpw = Encryption.CreateMD5HAsh(inputUser.Password);

            if (mongoUser is null || hashedpw != mongoUser.Password.ToUpper()) 
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

            string pwToBeHashed = user.Password;
            user.Password = Encryption.CreateMD5HAsh(pwToBeHashed);

            try
            {
                mongo.InsertRecord<User>("Customer", user);
                return Ok("User created in the temp list.");
            }
            catch (Exception) 
            {
                return Conflict("Conflict happend during user insertion");
            }

        }

    }
}
