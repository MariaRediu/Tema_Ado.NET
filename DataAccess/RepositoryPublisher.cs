using DataAccessConnection;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryPublisher
    {
        public int NrOfRowsFromPublisher()
        {
            var query = "select COUNT(PublisherId), Name from Publisher";

            using (var connection = ConnectionManager.GetSqlConnection())
            {

                SqlCommand commandRows = new SqlCommand(query, connection);
               
                int count = (int)commandRows.ExecuteScalar();
                Console.WriteLine($"Number of rows from the Publisher table is {count}");

                return count;
            }

        }

        public List<Publisher> SelectTop10()
        {
            List<Publisher> pubs = new List<Publisher>();
            string query = "SELECT TOP 10 *FROM  Publisher;";
            using (var connection = ConnectionManager.GetSqlConnection())
            {
                SqlCommand commandSelect = new SqlCommand(query, connection);
               
                using (SqlDataReader reader = commandSelect.ExecuteReader())
                {
                    var moreResults = true;
                    while (reader.Read())
                    {
                        var currentRow = reader;
                        Publisher pub = new Publisher();
                        pub.PublisherId =currentRow["PublisherId"] as int? ?? default(int); ;
                        pub.Name = (string)currentRow["Name"];

                        Console.WriteLine($"{pub.PublisherId}--{pub.Name}");

                        pubs.Add(pub);
                    }
                    moreResults = reader.NextResult();
                }
                return pubs;
            }
        }
        public List<Publisher> SelectNumberOfBook()
        {
            List<Publisher> pubs = new List<Publisher>();
            string query = "select p.PublisherId, p.Name ,COUNT(p.PublisherId) as NumberOfBook"
                            + "from[Publisher] as p "
                            + " join Book as b on b.PublisherId = p.PublisherId"
                            + "group by p.PublisherId, p.Name";
            using (var connection = ConnectionManager.GetSqlConnection())
            {
                SqlCommand commandNumber = new SqlCommand(query, connection);
               
                using (SqlDataReader reader = commandNumber.ExecuteReader())
                {
                    var moreResults = true;
                    while (reader.Read())
                    {

                        var row = reader;
                        Publisher pub = new Publisher();
                        pub.PublisherId = (int)row["PublisherId"];
                        pub.Name = (string)row["Name"];
                        var count = (int)row["NumberOfBook"];
                        Console.WriteLine($"{pub.PublisherId}-{pub.Name}-{count}");
                        pubs.Add(pub);
                    }
                    moreResults = reader.NextResult();
                }
                return pubs;
            }
        }
        public List<Publisher> CalculatePrice()
        {
            List<Publisher> pubs = new List<Publisher>();

            string query = "select  PublisherId,sum(Price) as SumOfPrice "
                            + "from Book group by PublisherId";
            using (var connection = ConnectionManager.GetSqlConnection())
            {
                SqlCommand commandPrice = new SqlCommand(query, connection);
              
                using (SqlDataReader reader = commandPrice.ExecuteReader())
                {
                    var moreResults = true;
                    while (reader.Read())
                    {
                        var row = reader;
                        Publisher pub = new Publisher();
                        pub.PublisherId = (int)row["PublisherId"];
                        int price = row["SumOfPrice"] as int? ?? default(int);
                        Console.WriteLine($"The price of books for publisher with id {pub.PublisherId} is {price}");
                        pubs.Add(pub);
                    }
                    moreResults = reader.NextResult();
                }
                return pubs;

            }
        }
    }
}

