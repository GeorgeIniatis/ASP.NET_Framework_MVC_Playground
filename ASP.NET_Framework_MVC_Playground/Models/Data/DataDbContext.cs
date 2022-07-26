using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Models.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base("name = DataDb") { }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MovieCustomer> MovieCustomers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Log Model
            modelBuilder.Entity<Log>()
               .HasKey(l => l.Log_ID)
               .Property(l => l.Log_ID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
               .HasColumnType("int");

            modelBuilder.Entity<Log>()
                .Property(l => l.Log_DateTime)
                .HasColumnType("datetime")
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(l => l.Log_Thread)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(l => l.Log_Level)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Log>()
               .Property(l => l.Log_Source)
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired();

            modelBuilder.Entity<Log>()
               .Property(l => l.Log_Message)
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired();

            modelBuilder.Entity<Log>()
               .Property(l => l.Log_Exception)
               .HasColumnType("varchar")
               .HasMaxLength(100);

            // Movie Model
            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieID)
                .Property(m => m.MovieID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnType("int");

            modelBuilder.Entity<Movie>()
                .Property(m => m.Movie_Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(m => m.Movie_Image)
                .HasColumnType("varbinary(MAX)")
                .IsRequired();

            modelBuilder.Entity<Movie>()
                    .Property(m => m.TinyMCE)
                    .HasColumnType("varchar")
                    .HasMaxLength(2000);

            // Customer Model
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID)
                .Property(c => c.CustomerID)
                .HasColumnType("nvarchar");

            // MovieCustomer Model
            modelBuilder.Entity<MovieCustomer>()
               .HasKey(c => new { c.MovieID, c.CustomerID });

            modelBuilder.Entity<MovieCustomer>()
                .Property(c => c.Rent_Date)
                .HasColumnType("datetime")
                .IsRequired();

            // Relationships
            // One movie can be rented to many customers
            modelBuilder.Entity<MovieCustomer>()
                .HasRequired(mc => mc.Movie)
                .WithMany(m => m.MovieCustomers)
                .HasForeignKey(mc => mc.MovieID)
                .WillCascadeOnDelete();

            // One customer can rent many movies
            modelBuilder.Entity<MovieCustomer>()
                .HasRequired(mc => mc.Customer)
                .WithMany(c => c.MovieCustomers)
                .HasForeignKey(mc => mc.CustomerID)
                .WillCascadeOnDelete();
        }
    }
}