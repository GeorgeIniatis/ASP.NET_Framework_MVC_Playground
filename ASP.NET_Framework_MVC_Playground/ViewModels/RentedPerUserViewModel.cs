using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Framework_MVC_Playground.ViewModels
{
    public class RentedPerUserViewModel
    {
        public string SelectedEmail { get; set; }
        public string SelectedEmail_UserID { get; set; }
        public List<SelectListItem> UserIDs { get; set; }
        public Dictionary<string, DateTime> RentedMovies { get; set; }
    }
}