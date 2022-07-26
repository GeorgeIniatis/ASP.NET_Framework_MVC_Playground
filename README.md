## ASP.NET Framework MVC Playground

This is just a playground site where multiple ASP.NET Framework, Javascript and Bootstrap elements have been implemented as a way to get familiar with them.

### Running Site Locally

**Assuming you are using Visual Studio 2019 or Visual Studio 2022**

1. Create a `connections.config` file that holds two connection strings that will connect our site to
our data and identity databases

```
// File Format
<connectionStrings>
  <add name="DataDb" connectionString="ConnectionString1" providerName="System.Data.SqlClient" />
  <add name="IdentityDb" connectionString="ConnectionString2" providerName="System.Data.SqlClient" />
</connectionStrings>
```

2. Create a `secrets.json` that holds all the required API keys 

```
// File Format
{
  "Google Authentication": {
    "client_id": "client_id",
    "client_secret": "client_secret"
  },
  "Google Translation": {
    "GoogleApiKey": "GoogleApiKey"
  },
  "Facebook Authentication": {
    "AppID": "AppID",
    "AppSecret": "AppSecret"
  },
  "Twitter Authentication": {
    "ApiKey": "ApiKey",
    "ApiKeySecret": "ApiKeySecret",
    "BearerToken": "BearerToken",
    "ClientID": "ClientID",
    "ClientSecret": "ClientSecret"
  },
  "Microsoft Authentication": {
    "ClientID": "ClientID",
    "ClientSecret": "ClientSecret"
  },
  "Sendgrid": {
    "send_grid_api_key": "send_grid_api_key"
  },
  "Twilio": {
    "AccountSID": "AccountSID",
    "AuthToken": "AuthToken",
    "PhoneNumber": "+PhoneNumber"
  },
  "Admin": {
    "Email": "admin@site.com",
    "Password": "test1234%^#",
    "FirstName": "Admin",
    "LastName":  "McAdmin"
  }
}
```

3. Data Migrations

```
enable-Migrations -ContextTypeName DataDbContext -MigrationsDirectory Models\Data\Migrations -Force
add-migration -ConfigurationTypeName ASP.NET_Framework_MVC_Playground.Models.Data.Migrations.Configuration initialCreate
update-database -ConfigurationTypeName ASP.NET_Framework_MVC_Playground.Models.Data.Migrations.Configuration
```

4. Identity Migrations

```
enable-Migrations -ContextTypeName ApplicationDbContext -MigrationsDirectory Models\Identity\Migrations -Force
add-migration -ConfigurationTypeName ASP.NET_Framework_MVC_Playground.Models.Identity.Migrations.Configuration initialCreate
update-database -ConfigurationTypeName ASP.NET_Framework_MVC_Playground.Models.Identity.Migrations.Configuration
```

5. Access Site

**You can register a normal user and sign in but you will only have access to a limited number of pages or**

**Login with admin user automatically created to access all the available pages**

```
Username: admin@site.com
Password: test1234%^#
```

6. Disclaimers

**Only the Add Movie Admin Page has been setup fully for Localisation and Globalization but without the database table changing the locale will have almost no effect except showing the variable names used**

**The SMS service will only work with number you have chosen during the set up of Sendgrid.**

**The Email service may or may not work. What I would suggest is using a temp email.**
