using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class InterestTag
    {
        public int InterestTagId { get; set; }
        public string FieldOfLife { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
