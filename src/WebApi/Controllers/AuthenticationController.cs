using Domain.DataTransferObjects.User;
using Domain.Services.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Extensions.ModelState;
using WebApi.Models.Configurations;
using WebApi.ViewModels.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly LoginService _LoginService;
        private readonly JwtConfiguration _JwtConfiguration;

        public AuthenticationController(LoginService loginService,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _LoginService = loginService;
            _JwtConfiguration = jwtConfiguration.Value;
        }

        [HttpPost]
        [Route("login-with-viewmodel")]
        public async Task<IActionResult> LoginWithViewModel([FromBody] UserLoginRequestViewModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            var userDTO = UserLoginRequestViewModel.Mapper.ToDTO(request);
            string token = _LoginService.GenerateToken(userDTO, _JwtConfiguration.ValidIssuer, _JwtConfiguration.Secret);
            return Ok(request);
        }

        [HttpPost]
        [Route("login-with-dto")]
        public async Task<IActionResult> LoginWithDTO([FromBody] UserLoginRequestDTO request)
        {
            string token = _LoginService.GenerateToken(request, _JwtConfiguration.ValidIssuer, _JwtConfiguration.Secret);
            return Ok(request);
        }
    }
}
