using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dtos
{
    public class MessageDto
    {
        public string Text { get; set; }
        public DateTime SentTime { get; set; }
        public string User { get; set; }
    }
}
