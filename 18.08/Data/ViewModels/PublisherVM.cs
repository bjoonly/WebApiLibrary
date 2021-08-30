using _18._08.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }
    
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }

    }
    public class PublisherResultVM
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previos { get; set; }
        public List<PublisherVM> Publishers { get; set; }
    }
}
