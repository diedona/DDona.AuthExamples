using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions.ModelState;
using WebApi.ViewModels.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        [Route("login-with-viewmodel")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestViewModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            return Ok(request);
        }
    }
}
