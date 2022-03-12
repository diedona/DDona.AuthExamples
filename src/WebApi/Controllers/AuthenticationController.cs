using Domain.DataTransferObjects.User;
using Domain.Services.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApi.Extensions.ModelState;
using WebApi.Models.Configurations;
using WebApi.ViewModels.User;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _AuthenticationService;
        private readonly JwtConfiguration _JwtConfiguration;

        public AuthenticationController(AuthenticationService loginService,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _AuthenticationService = loginService;
            _JwtConfiguration = jwtConfiguration.Value;
        }

        [HttpPost]
        [Route("login-with-viewmodel")]
        public async Task<IActionResult> LoginWithViewModel([FromBody] UserLoginRequestViewModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            var userDTO = UserLoginRequestViewModel.Mapper.ToDTO(request);
            string token = AuthorizeUser(userDTO);
            return Ok(new { token, username = request.Username });
        }

        [HttpPost]
        [Route("login-with-dto")]
        public async Task<IActionResult> LoginWithDTO([FromBody] UserLoginRequestDTO request)
        {
            string token = AuthorizeUser(request);
            return Ok(new { token, username = request.Username });
        }

        [HttpGet]
        [Route("who-am-i")]
        [Authorize]
        public async Task<IActionResult> WhoAmI()
        {
            string? username = this.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
            return Ok(username);
        }

        private string AuthorizeUser(UserLoginRequestDTO userDTO)
        {
            string token = _AuthenticationService.AuthorizeUser(userDTO, _JwtConfiguration.ValidIssuer, _JwtConfiguration.ValidAudience, _JwtConfiguration.Secret, _JwtConfiguration.LifeSpan);
            if (_AuthenticationService.Errors.Any())
                throw new Exception(_AuthenticationService.Errors.FirstOrDefault());

            return token;
        }
    }
}
