using Messages;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/online")]
    public class DownOrNotController : Controller
    {
        private readonly IMessageSession _messageSession;
        public DownOrNotController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<string> SendMessage()
        {
            var message = new IsItDown();
            await _messageSession.Send(message).ConfigureAwait(false);
            return "Message sent succesfully.";
        }
    }
}
