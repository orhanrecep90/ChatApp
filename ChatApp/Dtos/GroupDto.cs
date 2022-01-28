using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dtos
{
    public class GroupDto
    {
        public string GroupName { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
