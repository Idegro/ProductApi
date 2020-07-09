using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using NServiceBus;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
