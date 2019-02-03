using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private ClientSettings _clientSettings;

        public SettingsController(IOptions<ClientSettings> clientOptions)
        {
            _clientSettings = clientOptions.Value;
        }

        [HttpGet]
        public ClientSettings GetClientSettings()
        {
            return _clientSettings;
        }
    }
}