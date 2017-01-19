using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class ListOfPlacesModel
    {
        public List<PlaceModel> PlacesList { get; set; }

        public ListOfPlacesModel()
        {
            this.PlacesList = new List<PlaceModel>();
        }

    }
}