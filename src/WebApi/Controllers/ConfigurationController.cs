using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models.Configurations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly GeneralConfiguration _GeneralConfiguration;

        public ConfigurationController(IOptions<GeneralConfiguration> configuration)
        {
            _GeneralConfiguration = configuration.Value;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(_GeneralConfiguration.SecurityPhrase);
        }
    }
}
