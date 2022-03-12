using Domain.DataTransferObjects.User;
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
        public async Task<IActionResult> LoginWithViewModel([FromBody] UserLoginRequestViewModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            return Ok(request);
        }

        [HttpPost]
        [Route("login-with-dto")]
        public async Task<IActionResult> LoginWithDTO([FromBody] UserLoginRequestDTO request)
        {
            return Ok(request);
        }
    }
}
