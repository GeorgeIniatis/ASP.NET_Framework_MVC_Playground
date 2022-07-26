using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ASP.NET_Framework_MVC_Playground.Models.Data;

namespace ASP.NET_Framework_MVC_Playground.ViewModels
{
    public class MovieImageViewModel
    {
        public Movie Movie { get; set; }

        [DataType(DataType.Upload)]
        public string Image { get; set; }

    }
}