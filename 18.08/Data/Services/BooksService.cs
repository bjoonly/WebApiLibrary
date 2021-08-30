using _18._08.Data.Models;
using _18._08.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {

            var _book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.DateRead,
                Rate = bookVM.Rate,
                Genre = bookVM.Genre,
                ImageURL = bookVM.ImageURL,
                DateAdded = bookVM.DateAdded,
                PublisherId = bookVM.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
            if (bookVM.AuthorsId !=null)
            {
                foreach (var id in bookVM.AuthorsId)
                {
                    var _book_author = new BookAuthor()
                    {
                        BookId = _book.Id,
                        AuthorId = id
                    };
                    _context.BookAuthors.Add(_book_author);
                    _context.SaveChanges();
                }
            }
        }
        //public Book GetBookById(int id)
        //{
        //    return _context.Books.FirstOrDefault(b => b.Id == id);

        //}
        public IEnumerable<Book> GetBooks()
        {
            return _context.Books;
        }
        public BookWithAuthorsWM GetBookWithAuthorsById(int id)
        {
            var _book = _context.Books.Where(b => b.Id == id).Select(b => new BookWithAuthorsWM()
            {
                Title = b.Title,
                Description = b.Description,
                IsRead = b.IsRead,
                DateRead = b.DateRead,
                Rate = b.Rate,
                Genre = b.Genre,
                ImageURL = b.ImageURL,
                DateAdded = b.DateAdded,
                PublisherName = b.Publisher.Name,
                AuthorsName = b.BookAuthors.Select(ba => ba.Author.FullName).ToList()
            }).FirstOrDefault();

            return _book;
        }
        public Book EditBook(int id, BookVM bookVM)
        {
            Book _book = _context.Books.FirstOrDefault(b => id == b.Id);

            if (_book != null)
            {

                _book.Title = bookVM.Title;
                _book.Description = bookVM.Description;
                _book.IsRead = bookVM.IsRead;
                _book.DateRead = bookVM.DateRead;
                _book.Rate = bookVM.Rate;
                _book.Genre = bookVM.Genre;
                _book.ImageURL = bookVM.ImageURL;
                _book.DateAdded = bookVM.DateAdded;
                _book.PublisherId = bookVM.PublisherId;


                _context.SaveChanges();

            }

            return _book;
        }
        public void DeleteBook(int id)
        {
            Book _book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (_book != null)
            {

                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The book with id: {id} not found.");
            }
        }
    }
}
