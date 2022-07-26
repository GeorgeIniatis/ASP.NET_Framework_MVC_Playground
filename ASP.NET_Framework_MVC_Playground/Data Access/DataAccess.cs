using ASP.NET_Framework_MVC_Playground.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Data_Access
{
    // Methods that can be used without the Entity Framework
    // Currently not used
    public static class DataAccess
    {
        public static string GetConnectionString(string connectionName = "DataDb")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static void InsertRent(int movieID, string customerID, DateTime rentDate)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "InsertRent";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    com.Parameters.AddWithValue("@MovieID", movieID);
                    com.Parameters.AddWithValue("@CustomerID", customerID);
                    com.Parameters.AddWithValue("@Rent_Date", rentDate.ToShortDateString());
                    com.ExecuteNonQuery();
                }
            }
        }

        public static List<Movie> LoadRentedMovies(string customerID)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "LoadRentedMovies";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    com.Parameters.AddWithValue("@CustomerID", customerID);

                    List<Movie> movies = new List<Movie>();
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                string movieName = reader.GetString(0);

                                Movie movie = new Movie
                                {
                                    Movie_Name = movieName
                                };

                                movies.Add(movie);
                            }
                            return movies;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static void InsertCustomer(string customerID)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "InsertCustomer";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    com.Parameters.AddWithValue("@CustomerID", customerID);
                    com.ExecuteNonQuery();
                }
            }
        }

        public static List<Customer> LoadCustomers()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "LoadCustomers";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    List<Customer> customers = new List<Customer>();
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                string customerID = reader.GetString(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);


                                Customer customer = new Customer
                                {
                                    CustomerID = customerID,
                                };
                                customers.Add(customer);
                            }
                            return customers;
                        }
                        return null;
                    }
                }
            }
        }

        // Currently Not Used
        public static int LoadLastCustomerID()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "LoadLastCustomerID";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var lastCustomerID = reader.GetInt32(0);
                                return lastCustomerID;
                            }
                        }
                        return 0;
                    }
                }
            }
        }

        public static void InsertMovie(int movieID, string name)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "InsertMovie";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    com.Parameters.AddWithValue("@MovieID", movieID);
                    com.Parameters.AddWithValue("@Name", name);
                    com.ExecuteNonQuery();
                }
            }
        }

        public static List<Movie> LoadMovies()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "LoadMovies";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Connection = con;

                    List<Movie> movies = new List<Movie>();
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                int movieID = reader.GetInt32(0);
                                string movie_name = reader.GetString(1);


                                Movie movie = new Movie
                                {
                                    MovieID = movieID,
                                    Movie_Name = movie_name,
                                };
                                movies.Add(movie);
                            }
                            return movies;
                        }
                        return null;
                    }
                }
            }
        }
    }
}