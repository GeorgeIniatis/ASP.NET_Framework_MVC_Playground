using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ASP.NET_Framework_MVC_Playground.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using ASP.NET_Framework_MVC_Playground.Data_Access;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace ASP.NET_Framework_MVC_Playground
{
    public class EmailService : IIdentityMessageService
    {
        public static readonly log4net.ILog log = LogHelper.GetLogger();

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var apiKey = (String)JSON_Access.LoadJSON("Secrets.json")["Sendgrid"]["send_grid_api_key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("jogeo98@hotmail.com", "George Iniatis");
            var subject = message.Subject;
            var to = new EmailAddress(message.Destination);
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            log.Info($"Email response {response.StatusCode}");
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public static readonly log4net.ILog log = LogHelper.GetLogger();

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            string accountSID = (String)JSON_Access.LoadJSON("Secrets.json")["Twilio"]["AccountSID"];
            string authToken = (String)JSON_Access.LoadJSON("Secrets.json")["Twilio"]["AuthToken"];
            string fromNumber = (String)JSON_Access.LoadJSON("Secrets.json")["Twilio"]["PhoneNumber"];

            if ((string.IsNullOrEmpty(accountSID)) || (string.IsNullOrEmpty(authToken)))
            {
                throw new Exception("Null or Invalid API Keys");
            }

            return ExecuteSms(accountSID, authToken, fromNumber, message);
        }

        private async Task ExecuteSms(string AccountSID, string AuthToken, string fromNumber, IdentityMessage message)
        {
            TwilioClient.Init(AccountSID, AuthToken);

            var response = await MessageResource.CreateAsync(
              to: new PhoneNumber(message.Destination),
              from: new PhoneNumber(fromNumber),
              body: message.Body);

            log.Info($"SMS response {response.Status}");
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
