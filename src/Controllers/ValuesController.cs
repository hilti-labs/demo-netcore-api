using Microsoft.AspNetCore.Mvc;

namespace demo_netcore_2x_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        // GET: api/Values
        [HttpGet]
        public IActionResult Get()
        {
            var result = new [] { "value1", "value2", "value3", "value4", "value5", "value6", "value7" };
            return Ok(result);
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var result = "value";
            return Ok(result);
        }
        
        // POST: api/Values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return CreatedAtAction("Get", new { id = 5 }, value);
        }
        
        // PUT: api/Values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return Ok(value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
