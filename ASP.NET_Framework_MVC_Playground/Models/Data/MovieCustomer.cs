using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Models.Data
{
    public class MovieCustomer
    {
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        public DateTime Rent_Date { get; set; }
    }
}