using Microsoft.AspNetCore.Mvc;
using MindMapAPI.Models;
using MindMapAPI.Util;

namespace MindMapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //Refactor to dependey injection
        MongoConnection db = new MongoConnection("MindMapDb");

        // GET: api/Test
        [HttpGet]
        public IActionResult Get()
        {
            return Ok( db.LoadRecords<object>("Project"));
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return null;
            //return Ok(db.LoadRecord<object>("Project", "ObjectId("5eecec2ed912e22f08b1cc07")"));
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] Workspace obj)
        {   
            //System.Console.WriteLine(obj);
            db.InsertRecord<Workspace>("Workspaces",obj);
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
