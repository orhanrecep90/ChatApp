using ChatApp.Dtos;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task AddToGroup(string groupName)
        {
            var messages = await _chatService.GetMessagesByGroup(groupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("AddToGroup", messages);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
   

        public Task SendMessageToGroup(string group, string user, string message)
        {
            _chatService.SaveMessage(group, new MessageDto { User = user, SentTime = DateTime.Now, Text = message });
            return Clients.Group(group).SendAsync("ReceiveMessage", user, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), message);
        }
    }
}
