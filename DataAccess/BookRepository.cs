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
    public class BookRepository
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                var query = "select * from Book";
                var connection = ConnectionManager.GetSqlConnection();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();


                while (dataReader.Read())
                {
                    var currentRow = dataReader;
                    Book book = new Book();
                    book.BookId = currentRow["BookId"] as int? ?? default(int);
                    book.Title = (string)currentRow["Title"];
                    book.PublisherId = currentRow["PublisherId"] as int? ?? default(int);
                    book.Year = currentRow["Year"] as int? ?? default(int);
                    if (!currentRow.IsDBNull(4))
                    {
                        book.Price = (decimal)currentRow["Price"];
                    }



                    books.Add(book);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return books;
        }
        public List<Book> SelectYear()
        {
            List<Book> books = new List<Book>();
            string query = " select Title, Year from Book where Year = 2010";

            using (var connection = ConnectionManager.GetSqlConnection())
            {
                SqlCommand commandSelect = new SqlCommand(query, connection);
            
                using (SqlDataReader reader = commandSelect.ExecuteReader())
                {
                    var moreResults = true;

                    while (reader.Read())
                    {
                        
                        var row = reader;
                        Book book = new Book();
                        book.Title = (string)row["Title"];
                         book.Year = (int)row["Year"];
                        Console.WriteLine($"{book.Title}-{book.Year}");

                        books.Add(book);
                    }
                    moreResults = reader.NextResult();
                }
            }
            return books;
        }
        public List<Book> SelectTop10()
        {
            List<Book> books = new List<Book>();
            string query = "SELECT TOP 10 *FROM  Book;";
            using (var connection = ConnectionManager.GetSqlConnection())
            {
                SqlCommand commandSelect = new SqlCommand(query, connection);
             
                using (SqlDataReader reader = commandSelect.ExecuteReader())
                {
                    var moreResults = true;
                    while (reader.Read())
                    {
                        var currentRow = reader;
                        Book book = new Book();
                        book.BookId = currentRow["BookId"] as int? ?? default(int);
                        book.Title = (string)currentRow["Title"];
                        book.PublisherId = currentRow["PublisherId"] as int? ?? default(int);
                        book.Year = currentRow["Year"] as int? ?? default(int);
                        if (!currentRow.IsDBNull(4))
                        {
                            book.Price = (decimal)currentRow["Price"];
                        }

                        Console.WriteLine($"{book.Title}-{ book.PublisherId}-{ book.Year}-{  book.Price}");

                        books.Add(book);
                    }
                    moreResults = reader.NextResult();
                }
                return books;
            }
        }
            public int InsertBook(Book book)
            {


            string query = "Insert into Book (Title ,PublisherId, Year,Price) Values(@0, @1, @2, @3);"
                                       + "select scope_identity();";
         
            var connection = ConnectionManager.GetSqlConnection();
            connection.Open();

            SqlCommand commandInsert = new SqlCommand(query, connection);

            SqlParameter titleParam = new SqlParameter("@0", book.Title);
            SqlParameter PublisherIdParam = new SqlParameter("@1", book.PublisherId);
            SqlParameter YearParam = new SqlParameter("@2", book.Year);
            SqlParameter PriceParam = new SqlParameter("@3", book.Price);

            commandInsert.Parameters.Add(titleParam);
            commandInsert.Parameters.Add(PublisherIdParam);
            commandInsert.Parameters.Add(YearParam);
            commandInsert.Parameters.Add(PriceParam);

            int newBookId = (int)commandInsert.ExecuteScalar();

            commandInsert.Dispose();
            connection.Close();
            connection.Dispose();

            return newBookId;
            }

         }

}
