using Microsoft.AspNetCore.Mvc;
using MindMapAPI;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace MindMapAPI.Controllers
{
    [ApiController]
    public class PracticeController : ControllerBase
    {
        // GET: api/Practice
        [Route("api/[controller]/Load")]
        [HttpGet]
        public IActionResult Load()  
        {
            StreamReader reader = new StreamReader("Util/json.json");
            string content = reader.ReadToEnd();
            var jsonToReturn = JObject.Parse(content);
            return Ok(jsonToReturn.ToString());
        }

        [Route("api/[controller]/{num}")]
        [HttpGet]
        public IActionResult Number(int num)
        {
            if (num % 2 == 0)
            {
                return Ok($"Your input number is {num}, which is even.");
            }
            return Ok($"Your input number is {num}, which is odd.");
        }

        // POST: api/Practice
        [Route("api/[controller]")]
        [HttpPost]
        public IActionResult Post([FromBody] dynamic value)
        {
            return Ok("You sent this over \r\n" + value.ToString());
        }
    }
}
