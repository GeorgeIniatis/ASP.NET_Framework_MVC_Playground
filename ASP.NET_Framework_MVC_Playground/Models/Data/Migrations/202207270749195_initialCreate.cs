namespace ASP.NET_Framework_MVC_Playground.Models.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.MovieCustomers",
                c => new
                    {
                        MovieID = c.Int(nullable: false),
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        Rent_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.CustomerID })
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        Movie_Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Movie_Image = c.Binary(nullable: false),
                        Movie_Trailer_Link = c.String(nullable: false, maxLength: 100, unicode: false),
                        TinyMCE = c.String(maxLength: 2000, unicode: false),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Log_ID = c.Int(nullable: false, identity: true),
                        Log_DateTime = c.DateTime(nullable: false),
                        Log_Thread = c.String(nullable: false, maxLength: 50, unicode: false),
                        Log_Level = c.String(nullable: false, maxLength: 50, unicode: false),
                        Log_Source = c.String(nullable: false, maxLength: 100, unicode: false),
                        Log_Message = c.String(nullable: false, maxLength: 100, unicode: false),
                        Log_Exception = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Log_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieCustomers", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieCustomers", "CustomerID", "dbo.Customers");
            DropIndex("dbo.MovieCustomers", new[] { "CustomerID" });
            DropIndex("dbo.MovieCustomers", new[] { "MovieID" });
            DropTable("dbo.Logs");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieCustomers");
            DropTable("dbo.Customers");
        }
    }
}
