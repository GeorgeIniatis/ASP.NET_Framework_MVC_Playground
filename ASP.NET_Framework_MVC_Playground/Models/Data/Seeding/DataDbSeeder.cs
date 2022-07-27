using ASP.NET_Framework_MVC_Playground.Data_Access;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Models.Data.Seeding
{
    public static class DataDbSeeder
    {
        public static void Seed()
        {
            using (var context = new DataDbContext())
            {
                JObject moviesJson = JObject.Parse(File.ReadAllText(HttpContext.Current.Server.MapPath("Models/Data/Seeding/Movies.json")));
                foreach (var movie in moviesJson)
                {
                    Movie newMovie = new Movie()
                    {
                        Movie_Name = movie.Key,
                        Movie_Image = Convert.FromBase64String((string)movie.Value.SelectToken("Image")),
                        Movie_Trailer_Link = (string)movie.Value.SelectToken("Link")
                    };
                    context.Movies.AddOrUpdate(x => x.Movie_Name, newMovie);
                }
                context.SaveChanges();
            }
        }
    }
}