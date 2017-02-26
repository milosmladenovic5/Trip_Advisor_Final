using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class MessageModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public string Text { get; set; }
        public string Subject { get; set; }
        
        public DateTime SendingDate { get; set; }

    }
}