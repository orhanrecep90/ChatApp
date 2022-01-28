using ChatApp.Models;
using ChatApp.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class LogService: ILogService
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public LogService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _messageCollection = database.GetCollection<Message>(databaseSettings.MessageCollectionName);

        }

        public async Task CreateAsync(Message message)
        {
            await _messageCollection.InsertOneAsync(message);

        }

    }
}
