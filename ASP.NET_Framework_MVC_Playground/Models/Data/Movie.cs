using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Properties;

namespace ASP.NET_Framework_MVC_Playground.Models.Data
{
    public class Movie
    {
        public Movie()
        {
            MovieCustomers = new HashSet<MovieCustomer>();
        }

        [Display(Name = "MovieID", ResourceType = typeof(MovieModel))]
        public int MovieID { get; set; }

        [Display(Name = "MovieName", ResourceType = typeof(MovieModel))]
        [Required(ErrorMessageResourceName = "MovieNameIsRequired", ErrorMessageResourceType = typeof(MovieModel))]
        public string Movie_Name { get; set; }

        [Display(Name = "MovieImage", ResourceType = typeof(MovieModel))]
        public byte[] Movie_Image { get; set; }

        [AllowHtml]
        public string TinyMCE { get; set; }
        
        public virtual ICollection<MovieCustomer> MovieCustomers { get; set; }
    }
}