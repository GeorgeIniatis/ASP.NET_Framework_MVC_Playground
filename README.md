## ASP.NET Framework MVC Playground

This is just a playground site where multiple ASP.NET Framework, Javascript and Bootstrap elements have been implemented as a way to get familiar with them.

### Running Site Locally

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

**If migrations exist you can skip to update-database step**

`enable-Migrations -ContextTypeName DataDbContext -MigrationsDirectory Models\Data\Migrations -Force`

`add-migration -ConfigurationTypeName Identity_Authentication_Demo.Models.Data.Migrations.Configuration initialCreate`

`update-database -ConfigurationTypeName Identity_Authentication_Demo.Models.Data.Migrations.Configuration`

4. Identity Migrations

**If migrations exist you can skip to update-database step**

`enable-Migrations -ContextTypeName ApplicationDbContext -MigrationsDirectory Models\Identity\Migrations -Force`

`add-migration -ConfigurationTypeName Identity_Authentication_Demo.Models.Identity.Migrations.Configuration initialCreate`

`update-database -ConfigurationTypeName Identity_Authentication_Demo.Models.Identity.Migrations.Configuration`

5. Access Site

**You can register a normal user and sign in but you will only have access to a limited number of pages or**

**Login with admin user automatically created to access all the available pages**

```
Username: admin@site.com
Password: test1234%^#
```

6. Disclaimer

**The SMS service will definetely not work. The system has been setup to only work with my number.**

**The Email service may or may not work. What I would suggest is using a temp email.**
