using ChatApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
   public interface IChatService
    {
        Task SaveMessage(string groupName, MessageDto message);
        Task<List<MessageDto>> GetMessagesByGroup(string groupName);
    }
}
