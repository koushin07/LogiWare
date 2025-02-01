using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiware.Application.DTOs
{
    public class NotificationDto
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
    }
}