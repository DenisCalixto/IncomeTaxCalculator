using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IncomeTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTaxController : ControllerBase
    {
        private readonly ILogger<IncomeTaxController> _logger;

        // GET: api/<IncomeTaxController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<IncomeTaxController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
