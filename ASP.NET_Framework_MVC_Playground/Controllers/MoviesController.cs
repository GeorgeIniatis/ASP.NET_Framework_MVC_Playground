using ASP.NET_Framework_MVC_Playground.Models.Data;
using ASP.NET_Framework_MVC_Playground.Models;
using ASP.NET_Framework_MVC_Playground.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.Entity.Migrations;

namespace ASP.NET_Framework_MVC_Playground.Controllers
{
    [RequireHttps]
    public class MoviesController : BaseController
    {
        public static readonly log4net.ILog log = LogHelper.GetLogger();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditMovieDetails(int id)
        {
            using (var context = new DataDbContext())
            {
                Movie movie = context.Movies.Find(id);
                if (movie != null)
                {
                    return View(movie);
                }
                else
                {
                    return new HttpNotFoundResult("This movie does not exist!");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditMovieDetails(int id, Movie movie)
        {
            using (var context = new DataDbContext())
            {
                if (id == movie.MovieID)
                {
                    Movie fullModel = context.Movies.Find(id);
                    if (movie.TinyMCE != null)
                    {
                        fullModel.TinyMCE = movie.TinyMCE;
                        context.Movies.AddOrUpdate(fullModel);
                        context.SaveChanges();
                    }
                }
                else
                {
                    return new HttpNotFoundResult("Movie ID and ID provided do not match");
                }
                return RedirectToAction("All");
            }
        }

        // ? = nullable
        public ActionResult MovieArguments(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            return Content(String.Format("pageIndex = {0}, sortBy = {1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:range(1,12)}/{phrase}")]
        public ActionResult ByReleaseDate(int year, int month, string phrase)
        {
            Regex regex = new Regex(@"test\d{2}");
            Match match = regex.Match(phrase);
            if (match.Success){
                return Content($"Match: {match.Value}");
            }
            return Content(String.Format("Date Supplied {0}/{1}", month, year));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddMovie(MovieImageViewModel model, HttpPostedFileBase usedForUncroppedImageExample)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DataDbContext())
                {
                    string base64 = Request.Form["imgCropped"];
                    byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

                    List<String> movieNames = (from m in context.Movies
                                               select m.Movie_Name).ToList();

                    if(!movieNames.Contains(model.Movie.Movie_Name))
                    {
                        var movie = new Movie()
                        {
                            Movie_Name = model.Movie.Movie_Name,
                            Movie_Image = bytes,
                            Movie_Trailer_Link = model.Movie.Movie_Trailer_Link

                        };
                        context.Movies.Add(movie); // Only in the memory atm
                        context.SaveChanges(); // Changes applied to DB
                        ViewBag.Status = "Movie added successfully!";

                    }
                    else
                    {
                        ViewBag.Status = "Cannot have duplicate movies!";
                    }
                    
                    // Uncropped image example
                    // Converting uploaded file to bytes array
                    //if (image != null)
                    //{
                    //    using (BinaryReader br = new BinaryReader(image.InputStream))
                    //    {
                    //        bytes = br.ReadBytes(image.ContentLength);
                    //    }
                        
                    //}
                    //else
                    //{
                    //    ViewBag.FileStatus = "Movie could not be added. Please try again";
                    //}
                }
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult> All()
        {
            ViewBag.rentedMovies = await LoadRentedMovies(User.Identity.GetUserId());

            using (var context = new DataDbContext())
            {
                List<Movie> movies = (from m in context.Movies
                                     select m).ToList();

                return View(movies);
            }
        }

        [Authorize]
        public ActionResult Rent(int movieID)
        {

            // Inserting
            // Using Entity Framework
            using (var context = new DataDbContext())
            {
                var movieCustomer = new MovieCustomer()
                {
                    MovieID = movieID,
                    CustomerID = User.Identity.GetUserId(),
                    Rent_Date = DateTime.Now
                };
                context.MovieCustomers.Add(movieCustomer); // Only in the memory atm
                context.SaveChanges(); // Changes applied to DB
            }

            // Updating
            // Using Entity Framework
            //using (var context = new EmployeesEntities())
            //{
            //    var userID = User.Identity.GetUserId();
            //    var movieCustomer = context.MovieCustomers.Where(s => s.CustomerID == userID).FirstOrDefault();
            //    movieCustomer.Rent_Date = DateTime.Parse("12/06/2022");
            //    context.SaveChanges();
            //}

            // Deleting
            // Using Entity Framework
            //using (var context = new EmployeesEntities())
            //{
            //    var userID = User.Identity.GetUserId();
            //    var movieCustomer = context.MovieCustomers.Where(s => s.CustomerID == userID).FirstOrDefault();
            //    context.MovieCustomers.Remove(movieCustomer);
            //    context.SaveChanges();
            //}

            // Eager Loading needs them same variable to match. Eg. The tables need to have both a customer_name
            //using (var context = new EmployeesEntities())
            //{
            //    var userID = User.Identity.GetUserId();
            //    var movieCustomer = context.MovieCustomers.Include("Customer").Where(s => s.CustomerID == userID).First();
            //    // do stuff with movieCustomer
            //    context.SaveChanges();
            //}

            // Using Traditional Stuff
            //InsertRent(movieID, User.Identity.GetUserId(), DateTime.Today);

            return RedirectToAction("All", "Movies");
        }

        [Authorize]
        public async Task<ActionResult> Rented()
        {
            // Traditional Stuff
            // List<MovieModel> rentedMovies = LoadRentedMovies(User.Identity.GetUserId());

            // Entity Framework Async
            var userID = User.Identity.GetUserId();
            Dictionary<string, DateTime> rentedMovies = await GetRentedMoviesAsync(userID);
            return View(rentedMovies);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RentedPerUser()
        {
            using (var context = new ApplicationDbContext())
            {
                RentedPerUserViewModel viewModel = new RentedPerUserViewModel();
                viewModel.UserIDs = new List<SelectListItem>();
                Dictionary<string, string> userIDs_and_emails = (from c in context.Users
                                                                 select new
                                                                 {
                                                                     UserID = c.Id,
                                                                     Email = c.Email
                                                                 }).ToDictionary(q => q.UserID, q => q.Email);

                foreach (KeyValuePair<string, string> entry in userIDs_and_emails)
                {
                    viewModel.UserIDs.Add(new SelectListItem { Text = entry.Value, Value = $"{entry.Value},{entry.Key}" });
                }

                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RentedPerUser(RentedPerUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                String[] email_userID = model.SelectedEmail_UserID.Split(',');
                model.SelectedEmail = email_userID[0];
                model.RentedMovies = await GetRentedMoviesAsync(email_userID[1]);

                return View(model);
            }
            return View();
        }

        // GET: Movies/Random
        public async Task<ActionResult> Random()
        {
            if(User.Identity.GetUserId() != null)
            {
                ViewBag.rentedMovies = await LoadRentedMovies(User.Identity.GetUserId());
            }

            using (var context = new DataDbContext())
            {
                List<Movie> movies = await (from m in context.Movies
                                            select m).ToListAsync();

                if (movies == null)
                {
                    log.Error("No movies returned");
                    return (null);
                }
                else
                {
                    var random = new Random();
                    int random_index = random.Next(movies.Count);

                    log.Info($"Random Movie Returned: {movies[random_index].Movie_Name}");
                    return View(movies[random_index]);
                }
            }
        }

        // Helper Functions
        public FileContentResult getImg(int movieID)
        {
            using (var context = new DataDbContext())
            {
                byte[] byteArray = (from m in context.Movies
                                    where m.MovieID == movieID
                                    select m.Movie_Image).First();

                if (byteArray != null)
                {
                    return new FileContentResult(byteArray, "image/jpeg");
                }
                return null;
            }
        }

        private static async Task<List<String>> LoadRentedMovies(string userID)
        {
            using (var context = new DataDbContext())
            {
                List<String> movieNames = await (from m in context.Movies
                                           join mc in context.MovieCustomers on m.MovieID equals mc.MovieID
                                           where mc.CustomerID == userID
                                           select m.Movie_Name).ToListAsync();
                return movieNames;
            }
        }

        private static async Task<Dictionary<string, DateTime>> GetRentedMoviesAsync(string userID)
        {
            //List<Movy> rentedMovies = new List<Movy>();

            using (var context = new DataDbContext())
            {
                //var rentedMoviesID = await context.MovieCustomers.Where(s => s.CustomerID == userID).Select(s => s.MovieID).ToListAsync<int>();
                //rentedMovies = await context.Movies.Where(s => rentedMoviesID.Contains(s.MovieID)).ToListAsync<Movy>();

                Dictionary<string, DateTime> rentedMoviesOther = await (from mc in context.MovieCustomers
                                                                        join m in context.Movies
                                                                        on mc.MovieID equals m.MovieID
                                                                        where mc.CustomerID == userID
                                                                        select new
                                                                        {
                                                                            Movie_Name = m.Movie_Name,
                                                                            Rent_Date = mc.Rent_Date
                                                                        }).ToDictionaryAsync(q => q.Movie_Name, q => q.Rent_Date);

                return rentedMoviesOther;
            }
        }
    }
}