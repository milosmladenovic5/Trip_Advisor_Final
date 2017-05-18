using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class Message
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public int MessageId { get; set; }

        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public string Text { get; set; }
        public string Subject { get; set; }
        public bool Seen { get; set; }

        public long SendingDate { get; set; }
    }
}
