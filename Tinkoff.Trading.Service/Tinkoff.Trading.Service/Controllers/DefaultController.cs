using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Tinkoff.Trading.Service.Controllers
{
    [Controller]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        private string token = "";
        [HttpGet]
        public async Task PrintPercentsBySectors()
        {
            _logger.LogInformation($"Вызван метод {nameof(PrintPercentsBySectors)}");
             await new FirstAttempt(token).GetPortfolio();
        }
    }
}
