using AutoMapper;
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
        private readonly IMapper _Mapper;

        public AuthenticationController(AuthenticationService loginService,
            IOptions<JwtConfiguration> jwtConfiguration, 
            IMapper mapper)
        {
            _AuthenticationService = loginService;
            _JwtConfiguration = jwtConfiguration.Value;
            _Mapper = mapper;
        }

        [HttpPost]
        [Route("login-with-viewmodel")]
        public async Task<IActionResult> LoginWithViewModel([FromBody] UserLoginRequestViewModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            var userDTO = _Mapper.Map<UserLoginRequestDTO>(request);
            string? token = await AuthorizeUser(userDTO);

            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            return Ok(new { token, username = request.Username });
        }

        [HttpPost]
        [Route("login-with-dto")]
        public async Task<IActionResult> LoginWithDTO([FromBody] UserLoginRequestDTO request)
        {
            string? token = await AuthorizeUser(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetAllErrors());

            return Ok(new { token, username = request.Username });
        }

        [HttpGet]
        [Route("who-am-i")]
        [Authorize]
        public IActionResult WhoAmI()
        {
            string? username = this.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
            return Ok(username);
        }

        private async Task<string?> AuthorizeUser(UserLoginRequestDTO userDTO)
        {
            string? token = await _AuthenticationService.AuthorizeUser(userDTO, _JwtConfiguration.ValidIssuer, _JwtConfiguration.ValidAudience, _JwtConfiguration.Secret, _JwtConfiguration.LifeSpan);
            if (_AuthenticationService.Errors.Any())
                ModelState.AddModelError("domain", _AuthenticationService.Errors.First());

            return token;
        }
    }
}
