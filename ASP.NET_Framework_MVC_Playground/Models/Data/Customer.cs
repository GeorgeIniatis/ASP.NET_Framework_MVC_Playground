using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Models.Data
{
    public class Customer
    {
        public Customer()
        {
            MovieCustomers = new HashSet<MovieCustomer>();
        }

        public string CustomerID { get; set; }
        public virtual ICollection<MovieCustomer> MovieCustomers { get; set; }
    }
}