using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
   public interface ILogService
    {
        Task CreateAsync(Message message);
    }
}
