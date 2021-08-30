
using _18._08.Data;
using _18._08.Data.Models;
using _18._08.Data.Services;
using _18._08.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestWebApi
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> _dbContextOptions =
            new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Library").Options;

        private AppDbContext _context;
        private PublisherService _publisherService;
        private AuthorService _authorService;
        private BooksService _booksService;


        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publisherService = new PublisherService(_context);
            _authorService = new AuthorService(_context);
            _booksService = new BooksService(_context);
        }

        //Book
        #region Book
        [Test, Order(1)]
        public void GetBooks_Test()
        {
            var result = _booksService.GetBooks();
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test, Order(2)]
        public void GetBook_By_Id_With_Authors_Test()
        {
            var result = _booksService.GetBookWithAuthorsById(1);
            Assert.That(result.Title, Is.EqualTo("Book 1"));
            Assert.That(result.AuthorsName.Count, Is.EqualTo(1));
            Assert.That(result.AuthorsName.FirstOrDefault(), Is.EqualTo("Author 1"));
        }

        [Test, Order(3)]
        public void AddBook_Without_Authors_Test()
        {
            _booksService.AddBookWithAuthors(new BookVM() { Title = "Book 5", PublisherId = 1 });

            var result = _booksService.GetBookWithAuthorsById(5);
            Assert.That(result.Title, Is.EqualTo("Book 5"));
            Assert.That(result.AuthorsName.Count, Is.EqualTo(0));

        }

        [Test, Order(4)]
        public void AddBook_With_Authors_Test()
        {
            _booksService.AddBookWithAuthors(new BookVM() { Title = "Book 6", PublisherId = 1, AuthorsId = new List<int>() { 1 } });
            var result = _booksService.GetBookWithAuthorsById(6);

            Assert.That(result.Title, Is.EqualTo("Book 6"));
            Assert.That(result.AuthorsName.Count, Is.EqualTo(1));
        }
        [Test, Order(5)]
        public void EditBook_Test()
        {
            var result = _booksService.EditBook(3, new BookVM() { Title = "New title" });
            Assert.That(result.Title, Is.EqualTo("New title"));
        }

        [Test, Order(6)]
        public void DeleteBook_Test()
        {
            _booksService.DeleteBook(1);
            var result = _booksService.GetBookWithAuthorsById(1);
            Assert.That(result, Is.EqualTo(null));
        }

        #endregion

        //Publisher
        #region Publisher
        [Test, Order(7)]
        public void GetPublishers_WithNoSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publisherService.GetPublishers("", "", 0);

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test, Order(8)]
        public void GetPublishers_WithSort_WithNoSearch_WithNoPageNumber_Test()
        {
            var result = _publisherService.GetPublishers("desc", "", 0);

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 9"));
        }

        [Test, Order(9)]
        public void GetPublishers_WithNoSort_WithSearch_WithNoPageNumber_Test()
        {
            var result = _publisherService.GetPublishers("", "6", 0);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));

        }

        [Test, Order(10)]
        public void GetPublishers_WithSort_WithNoSearch_WithPageNumber_Test()
        {
            var result = _publisherService.GetPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(4));
        }
        [Test, Order(11)]
        public void GetPublisher_By_Id_Test()
        {
            var result = _publisherService.GetPublisherById(2);

            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test, Order(12)]
        public void GetPublisherData_Test()
        {
            var result = _publisherService.GetPublisherData(2);

            Assert.That(result.BookAuthors.Count, Is.EqualTo(1));

        }

        [Test, Order(13)]
        public void AddPublishers_Test()
        {
            var result = _publisherService.AddPublisher(new PublisherVM()
            {
                Name = "Publisher 11",
            });


            Assert.That(result.Id, Is.EqualTo(11));
            Assert.That(result.Name, Is.EqualTo("Publisher 11"));

        }

        [Test, Order(14)]
        public void DeletePublishers()
        {
            _publisherService.DeletePublisher(1);
            var result = _publisherService.GetPublisherById(1);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test, Order(15)]
        public void EditPublisher_Test()
        {
            var result = _publisherService.EditPublisher(3, new PublisherVM() { Name = "New name" });

            Assert.That(result.Id, Is.EqualTo(3));
            Assert.That(result.Name, Is.EqualTo("New name"));
        }
        #endregion
        //Author
        #region Author
        [Test, Order(16)]
        public void GetAuthors_Test()
        {
            var result = _authorService.GetAuthors();

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test, Order(17)]
        public void GetAuthor_By_Id_Test()
        {
            var result = _authorService.GetAuthorById(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.FullName, Is.EqualTo("Author 2"));
        }

        [Test, Order(18)]
        public void AddAuthor_Test()
        {
            var result = _authorService.AddAuthor(new AuthorVM()
            {
                FullName = "Author 4"
            });

            Assert.That(result.Id, Is.EqualTo(4));
            Assert.That(result.FullName, Is.EqualTo("Author 4"));
        }

        [Test, Order(19)]
        public void EditAuthor_Test()
        {
            var result = _authorService.EditAuthor(2, new AuthorVM()
            {
                FullName = "New full name"
            });

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.FullName, Is.EqualTo("New full name"));
        }


        [Test, Order(20)]
        public void GetAuthor_With_Books_Test()
        {
            var result = _authorService.GetAuthorWithBooks(1);


            Assert.That(result.FullName, Is.EqualTo("Author 1"));
            Assert.That(result.BookTitles.Count, Is.EqualTo(1));
        }

        [Test, Order(21)]
        public void DeleteAuthor_Test()
        {
            _authorService.DeleteAuthor(1);
            var result = _authorService.GetAuthorById(1);
            Assert.That(result, Is.EqualTo(null));
        }

        #endregion

        


        [OneTimeTearDown]
        public void CleanUp()
        {

            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher
                {
                    Id=1,
                    Name="Publisher 1"
                },
                 new Publisher
                {
                    Id=2,
                    Name="Publisher 2"               
                  
                },
                  new Publisher
                {
                    Id=3,
                    Name="Publisher 3"
                },
                   new Publisher
                {
                    Id=4,
                    Name="Publisher 4"
                },
                    new Publisher
                {
                    Id=5,
                    Name="Publisher 5"
                },
                     new Publisher
                {
                    Id=6,
                    Name="Publisher 6"
                },
                      new Publisher
                {
                    Id=7,
                    Name="Publisher 7"
                },
                      new Publisher
                {
                    Id=8,
                    Name="Publisher 8"
                },
                       new Publisher
                {
                    Id=9,
                    Name="Publisher 9"
                },
                        new Publisher
                {
                    Id=10,
                    Name="Publisher 10"
                }

            };

            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();


            var books = new List<Book>()
            {
                new Book()
                {
                     Title="Book 1",
                     PublisherId=2                                     
                },
                 new Book()
                {
                     Title="Book 2",
                     PublisherId=2
                },
                  new Book()
                {
                     Title="Book 3",
                     PublisherId=1
                },
                   new Book()
                {
                     Title="Book 4",
                     PublisherId=3
                },

            };
            _context.Books.AddRange(books);
            _context.SaveChanges();

            var auhtors = new List<Author>()
            {
                 new Author()
                 {
                       FullName="Author 1"
  
                 },
                   new Author()
                 {
                       FullName="Author 2"
                 },
                     new Author()
                 {
                       FullName="Author 3"
                 },
            };

            _context.Authors.AddRange(auhtors);
            _context.SaveChanges();

            var booksAuthors=new List<BookAuthor>(){
            new  BookAuthor()
            {
                 AuthorId=1,
                 BookId=1
            },
               new  BookAuthor()
            {
                 AuthorId=1,
                 BookId=2
            },
                  new  BookAuthor()
            {
                 AuthorId=2,
                 BookId=3
            }
            };
            _context.BookAuthors.AddRange(booksAuthors);
            _context.SaveChanges();

        }


    }
}