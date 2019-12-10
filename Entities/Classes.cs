using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataConnection2;

namespace Entities2
{
    public class Publisher 
    {
        public int PublisherId { get; set; }    
        public string Name { get; set; }
    }

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }   
        public int PublisherId { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }  
    }
    public class NumberOfBooksPerPublisher
    {
        public int NoOfBooks { get; set; }
        public string PublisherName { get; set; }
    }
}
