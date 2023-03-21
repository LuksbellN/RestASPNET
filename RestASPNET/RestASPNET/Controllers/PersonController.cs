using Microsoft.AspNetCore.Mvc;
using RestASPNET.Model;
using RestASPNET.Business;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("v{version:apiVersion}/api/[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<CalculatorController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personBusiness.AllAsync());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if(person == null) return NotFound();
            return Ok(person);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if(person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if(person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
