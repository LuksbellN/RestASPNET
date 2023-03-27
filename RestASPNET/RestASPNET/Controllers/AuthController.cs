using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestASPNET.Business;
using RestASPNET.Data.VO;

namespace RestASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if (token == null) return BadRequest("Invalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}
