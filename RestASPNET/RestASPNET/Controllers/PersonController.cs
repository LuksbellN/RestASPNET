using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.Business;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.FIlters;

namespace RestASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
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

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get(
            [FromQuery] string name,
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if(person == null) return NotFound();
            return Ok(person);
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var persons = _personBusiness.FindByName(firstName, lastName);
            if(persons == null) return NotFound();
            return Ok(persons);
        }
        
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if(person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }
        
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if(person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Patch(int id)
        {
            var person = _personBusiness.Disable(id);
            if(person == null) return NotFound();
            return Ok(person);
        }
         
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
