using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities2;
using DataConnection2;



namespace PublisherRepository
{
    public class PublisherMethods
    {
        public static void InsertRow()
        {
            var connection = ConnectionManager.GetConnection();
            Console.Write("Itroduceti datele:");
            var data = Console.ReadLine();
            SqlParameter param = new SqlParameter("@data", data);
            var query = "insert into Publisher Values" + @data;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(param);
            command.ExecuteNonQuery();
        }
        public static void Count()
        {
            var connection = ConnectionManager.GetConnection();
            var query = "select Count(PublisherId) from Publisher";
            SqlCommand command = new SqlCommand(query, connection);
            var result = command.ExecuteScalar();
            Console.WriteLine($"Number of rows from Publisher is {result}");
        }
        public static void Top10Publishers()
        {
            var connection = ConnectionManager.GetConnection();
            SqlDataReader reader = null;
            Console.WriteLine("The first 10 Publishers from the table are:");
            var query = "select * from Publisher where PublisherId between 1 and 10";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Publisher publisher = new Publisher();
                    publisher.PublisherId= (int)reader["PublisherId"];
                    publisher.Name = (string)reader["Name"];
                    Console.WriteLine($"PublisherId: {publisher.PublisherId}  Name: {publisher.Name}");

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public static void BooksPerPublisher()
        {
            Console.WriteLine("Number of books published by every Publisher:");
            SqlDataReader reader = null;
            var connection = ConnectionManager.GetConnection();
            var query = "select p.[Name] ,count(b.[PublisherId]) as BooksPerPublisher from Publisher p, Book b where p.PublisherId = b.PublisherId group by b.PublisherId,p.[Name]";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    Publisher publisher = new Publisher();
                    publisher.Name = (string)reader["Name"];
                    int NrOfBooks = (int)reader["BooksPerPublisher"];
                    Console.WriteLine($"Publisher {publisher.Name} wrote {publisher.PublisherId} books.");

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public static void TotalPricePerPublisher()
        {
            var connection = ConnectionManager.GetConnection();
            Console.Write("Insert PublisherId to calculate the total price for his books: ");
            var id = int.Parse(Console.ReadLine());
            SqlParameter param = new SqlParameter("@id", id);
            var query = "select sum(Price) from Book where Book.PublisherId = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(param);
            var result = command.ExecuteScalar();
            Console.WriteLine($"Total price for PublisherId {id} is {result}");
        }

        public static void NumberOfBooksPerPublisher()
        {
            SqlDataReader reader = null;
            var connection = ConnectionManager.GetConnection();
            var query = "select p.[Name] ,count(b.[PublisherId]) as BooksPerPublisher from Publisher p, Book b where p.PublisherId = b.PublisherId group by b.PublisherId,p.[Name]";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                reader = command.ExecuteReader();
                List<NumberOfBooksPerPublisher> lista = new List<NumberOfBooksPerPublisher>();
                while (reader.Read())
                {
                    NumberOfBooksPerPublisher booksPerPublisher = new NumberOfBooksPerPublisher();
                    booksPerPublisher.PublisherName= (string)reader["Name"];
                    booksPerPublisher.NoOfBooks = (int)reader["BooksPerPublisher"];
                    lista.Add(booksPerPublisher);
                }
                foreach (var item in lista)
                {
                    Console.WriteLine($"{item.PublisherName}  {item.NoOfBooks}");
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }


    }

}
