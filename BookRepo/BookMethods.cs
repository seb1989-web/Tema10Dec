using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities2;
using DataConnection2;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BookRepository
{
    public class BookMethods
    {
        public static void InsertRow()
        {
            var connection = ConnectionManager.GetConnection();
            Console.Write("Introduceti datele:");
            var data = Console.ReadLine();
            SqlParameter param = new SqlParameter("@data", data);
            var query = "insert into Book Values" + @data;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(param);
            command.ExecuteNonQuery();
        }
        public static void Books2010()
        {
            SqlDataReader reader = null;
            var connection = ConnectionManager.GetConnection();
            Console.Write("Afiseaza toate cartile din anul:");
            var an = int.Parse(Console.ReadLine());
            SqlParameter param = new SqlParameter("@an", an);
            var query = "select * from Book where Year = @an";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(param);
                reader = command.ExecuteReader();
                Console.WriteLine($"Toate cartile din anul {an} sunt:");
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = (int)reader["BookId"];
                    book.Title = (string)reader["Title"];
                    book.PublisherId = (int)reader["PublisherId"];
                    book.Year = (int)reader["Year"];
                    book.Price = (decimal)reader["Price"];
                    Console.WriteLine($"Title: {book.Title} PublisherId: {book.PublisherId} Year: {book.Year} Price: {book.Price}");

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
        public static void MaxYear()
        {
            SqlDataReader reader = null;
            var connection = ConnectionManager.GetConnection();
            Console.WriteLine("Cartea publicata cel mai recent este:");
            var query = "select * from Book where Year = (select max(Year) from Book)";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Title = (string)reader["Title"];
                    book.PublisherId = (int)reader["PublisherId"];
                    book.Year = (int)reader["Year"];
                    book.Price = (decimal)reader["Price"];
                    Console.WriteLine($"Title: {book.Title} PublisherId: {book.PublisherId} Year: {book.Year} Price: {book.Price}");
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
        public static void Top10Books()
        {
            var connection = ConnectionManager.GetConnection();
            SqlDataReader reader = null;
            Console.WriteLine("The first 10 books from the table are:");
            var query = $"select * from Book where BookId between 1 and 10";
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Title = (string)reader["Title"];
                    book.Year = (int)reader["Year"];
                    book.Price = (decimal)reader["Price"];
                    Console.WriteLine($"Title: {book.Title} Year: {book.Year} Price: {book.Price}");
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

        public static void SerializeJson()
        {
            var connection = ConnectionManager.GetConnection();
            SqlDataReader reader = null;
            var query = $"select * from Book";
            try
            {
                
                List<Book> lista = new List<Book>();
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = (int)reader["BookId"];
                    book.Title = (string)reader["Title"];
                    book.PublisherId = (int)reader["PublisherId"];
                    book.Year = (int)reader["Year"];
                    book.Price = (decimal)reader["Price"];
                    lista.Add(book);
                }
                foreach (var item in lista)
                {
                    Console.WriteLine($"{item.BookId}  {item.Title}  {item.PublisherId}  {item.Year}  {item.Price}  ");
                }
                string json = JsonConvert.SerializeObject(lista);
                Console.WriteLine(json);
                string path = @"C:\Users\seb\Desktop\download\jSon.txt";
                File.WriteAllText(path, json);

            }
            finally
            {
                if(reader != null)
                {
                    reader.Close();
                }
            }

        }
        public static void SerializeXML()
        {
            var connection = ConnectionManager.GetConnection();
            SqlDataReader reader = null;
            var query = $"select * from Book";
            try
            {

                List<Book> lista = new List<Book>();
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = (int)reader["BookId"];
                    book.Title = (string)reader["Title"];
                    book.PublisherId = (int)reader["PublisherId"];
                    book.Year = (int)reader["Year"];
                    book.Price = (decimal)reader["Price"];
                    lista.Add(book);
                }
                foreach (var item in lista)
                {
                    Console.WriteLine($"{item.BookId}  {item.Title}  {item.PublisherId}  {item.Year}  {item.Price}  ");
                }

                XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Book>));

                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, lista);
                        string xml = sww.ToString();
                        Console.WriteLine(xml);

                        string path = @"C:\Users\seb\Desktop\download\XML.txt";
                        File.WriteAllText(path, xml);

                    }
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