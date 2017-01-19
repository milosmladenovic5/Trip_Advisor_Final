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


        public string StatusName { get; set; }//recimo 1. Explorer , 2.Baby steps, 3.Ocassional traveler ili tome slicno, stagod 
        //ili nomad

       // public string StatusName { get; set; }      //recimo 1. Explorer , 2.Baby steps, 3.Ocassional traveler ili tome slicno, stagod 

        public string Description { get; set; }


    }
}
