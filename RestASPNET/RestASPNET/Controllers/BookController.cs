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
    public class BookController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<CalculatorController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]

        public async Task<IActionResult> Get()
        {
            return Ok(await _bookBusiness.AllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HypermediaFilter))]

        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [TypeFilter(typeof(HypermediaFilter))]

        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
