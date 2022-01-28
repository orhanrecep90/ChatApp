using ChatApp.Dtos;
using ChatApp.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatApp.Services
{

    public class ChatService : IChatService
    {
        private readonly RedisService _redisService;
        private readonly ILogService _logService;

        public ChatService(RedisService redisService, ILogService logService)
        {
            _redisService = redisService;
            _logService = logService;
        }


        public async Task SaveMessage(string groupName, MessageDto message)
        {
            await _logService.CreateAsync(new Message { GroupName = groupName, SentTime = message.SentTime, Text = message.Text, User = message.User });
            var result = await _redisService.GetDb().ListRightPushAsync(groupName, JsonSerializer.Serialize(message));

        }
        public async Task<List<MessageDto>> GetMessagesByGroup(string groupName)
        {
            List<MessageDto> result = new List<MessageDto>();

            var redisData = await _redisService.GetDb().ListRangeAsync(groupName);

            if (redisData != null)
            {
                redisData.ToList().ForEach(x =>
                {
                    var stringData = x.ToString();
                    result.Add(JsonSerializer.Deserialize<MessageDto>(stringData));
                });
            }
            return result;
        }
    }
}
