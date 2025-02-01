using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logiware.Application.DTOs;
using LogiWare.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace Logiware.API.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly ISiteService _siteService;

        public NotificationHub(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendNotification(NotificationDto  notification)
        {
            var receiver = await _siteService.GetSiteById(notification.ReceiverId);
            var sender = await _siteService.GetSiteById(notification.SenderId);
            if(receiver is null || sender is null) throw new HubException("no sender or receiver");
            await Clients.User(receiver.Id.ToString()).SendAsync("ReceiveNotification", notification);

            

        }

        
    }
}