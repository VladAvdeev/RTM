using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RTM.Common.Models;
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
        [HttpPost("send-notification")]
        public async Task SendNotification(int? userid, [FromBody]NotificationModel notification)
        { 
            if(userid.HasValue)
                await hub.Clients.Group(userid.ToString()).SendAsync("Receive", notification);
            else
                await hub.Clients.All.SendAsync("ReceiveAll", notification);
        }
    }
}
