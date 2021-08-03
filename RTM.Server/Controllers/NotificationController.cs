using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RTM.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTM.Server.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        IHubContext<NotificationHub> hub;
        public NotificationController(IHubContext<NotificationHub> hub)
        {
            this.hub = hub;
        }
        [HttpGet("SendAll")]
        public async Task SendAll(string message)
        { 
            await hub.Clients.All.SendAsync("ReceiveAll", message);
        }
    }
}
