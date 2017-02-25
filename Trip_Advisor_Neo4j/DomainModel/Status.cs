using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }

        public int StatusFLAG { get; set; }


    }
}
