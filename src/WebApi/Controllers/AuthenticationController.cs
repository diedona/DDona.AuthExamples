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
            var userDTO = _Mapper.Map<UserLoginRequestDTO>(request);
            string? token = await AuthorizeUser(userDTO);

            if (!ModelState.IsValid)
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

        [Authorize]
        [HttpGet]
        [Route("who-am-i")]
        public IActionResult WhoAmI()
        {
            string? username = this.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
            return Ok(username);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] UserCreateViewModel request)
        {
            var dto = _Mapper.Map<UserCreateDTO>(request);
            await _AuthenticationService.CreateNewUser(this.User.Identity!.Name!, dto);
            if (_AuthenticationService.Errors.Any())
                return BadRequest(_AuthenticationService.Errors.First());

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("inactivate-user/{userId}")]
        public async Task<ActionResult> InactivateUser(Guid userId)
        {
            await _AuthenticationService.InactivateUser(this.User.Identity!.Name!, userId);
            if (_AuthenticationService.Errors.Any())
                return BadRequest(_AuthenticationService.Errors.First());

            return Ok();
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
