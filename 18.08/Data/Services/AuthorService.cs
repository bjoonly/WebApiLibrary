using _18._08.Data.Models;
using _18._08.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Data.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(AuthorVM authorVM)
        {
            var _author = new Author()
            {
                FullName = authorVM.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();

            return _author;
        }
        public Author EditAuthor(int id, AuthorVM authorVM)
        {
            var _author = _context.Authors.FirstOrDefault(p => p.Id == id);

            if (_author != null)
            {

                _author.FullName = authorVM.FullName;
                _context.SaveChanges();
            }

            return _author;
        }
        public AuthorWithBooksVM GetAuthorWithBooks(int id)
        {
            var author = _context.Authors.Where(a => a.Id == id).Select(a => new AuthorWithBooksVM()
            {
                FullName = a.FullName,
                BookTitles = a.BookAuthors.Select(ba => ba.Book.Title).ToList()
            }).FirstOrDefault();
            return author;

        }
        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors;
        }
        public Author GetAuthorById(int id)
        {
            return _context.Authors.FirstOrDefault(p => p.Id == id);

        }
        public void DeleteAuthor(int id)
        {
            var _author = _context.Authors.FirstOrDefault(p => p.Id == id);
            if (_author != null)
            {

                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The author with id: {id} not found.");
            }
        }
    }
}
