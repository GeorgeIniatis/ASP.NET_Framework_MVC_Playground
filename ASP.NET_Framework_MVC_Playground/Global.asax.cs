using ASP.NET_Framework_MVC_Playground.Data_Access;
using ASP.NET_Framework_MVC_Playground.Models;
using ASP.NET_Framework_MVC_Playground.Models.Data;
using ASP.NET_Framework_MVC_Playground.Models.Data.Seeding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Westwind.Globalization;
using Westwind.Utilities;

namespace ASP.NET_Framework_MVC_Playground
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly log4net.ILog log = LogHelper.GetLogger();

        protected void Application_BeginRequest()
        {
            // Automatically set the user's locale to what the browser returns
            // and optionally only allow certain locales/locale-prefixes
            WebUtils.SetUserLocale(allowedLocales: "en-US,en-UK,el-GR");
        }

        protected async void Application_Start()
        {
            // Westwind Globalization Stuff
            var config = DbResourceConfiguration.Current;

            config.ConnectionString = "DataDb";
            config.DbResourceDataManagerType = typeof(DbResourceSqlServerDataManager);
            config.GoogleApiKey = (String)JSON_Access.LoadJSON("Secrets.json")["Google Translation"]["GoogleApiKey"];

            DataDbSeeder.Seed();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create roles if they do not exist
            await createRoles();
            //Create admin user if it does not exist
            await createAdmin();
        }

        private async Task createRoles()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!await RoleManager.RoleExistsAsync("Admin"))
            {
                var roleresult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
                log.Info(String.Format("Admin Role Created:{0}", roleresult.Succeeded));

            }
            if (!await RoleManager.RoleExistsAsync("User"))
            {
                var roleresult = await RoleManager.CreateAsync(new IdentityRole("User"));
                log.Info(String.Format("User Role Created:{0}", roleresult.Succeeded));
            }
        }

        private async Task createAdmin()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = new ApplicationUser
            {
                UserName = (String)JSON_Access.LoadJSON("Secrets.json")["Admin"]["Email"],
                Email = (String)JSON_Access.LoadJSON("Secrets.json")["Admin"]["Email"],
                FirstName = (String)JSON_Access.LoadJSON("Secrets.json")["Admin"]["FirstName"],
                LastName = (String)JSON_Access.LoadJSON("Secrets.json")["Admin"]["LastName"],

            };
            var result = await userManager.CreateAsync(user, (String)JSON_Access.LoadJSON("Secrets.json")["Admin"]["Password"]);
            if (result.Succeeded)
            {
                // Assign role
                userManager.AddToRole(user.Id, "Admin");
                // Create customer
                using (var context = new DataDbContext())
                {
                    Customer customer = new Customer
                    {
                        CustomerID = user.Id
                    };
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }
            log.Info("Admin Role Created or Already Exists");
        }
    }
}
