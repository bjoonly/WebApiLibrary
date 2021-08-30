using _18._08.Data.Models;
using _18._08.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Data.Services
{
    public class PublisherService
    {
        private readonly AppDbContext _context;
        const int countPublisherOnPage = 6;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }
        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);

        }
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int id)
        {
            var _publisher = _context.Publishers.Where(p => p.Id == id)
                .Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b => new BookAuthorVM()
                    {
                        BookName = b.Title,
                        BookAuthors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();


            return _publisher;
        }

        public IEnumerable<Publisher> GetPublishers(string sortBy, string searchString, int pageNumber)
        {

            IEnumerable<Publisher> publishers = _context.Publishers.OrderBy(p => p.Name);
        
            if (!String.IsNullOrEmpty(searchString))
            {
                publishers = publishers.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }
            if (!String.IsNullOrEmpty(sortBy) && sortBy.ToLower() == "desc")
            {
                publishers = publishers.OrderByDescending(p => p.Name);
            }


            if (pageNumber > 0)
            {
                int skip = (pageNumber - 1) * countPublisherOnPage;
                publishers = publishers.Skip(skip).Take(countPublisherOnPage);
            }
            else if (pageNumber == 0)
            {
                publishers = publishers.Take(countPublisherOnPage);
            }
            else
            {
                throw new Exception("Invalid page number");
            }
            
            return publishers;


        }
        public Publisher EditPublisher(int id, PublisherVM publisherVM)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (_publisher != null)
            {

                _publisher.Name = publisherVM.Name;
                _context.SaveChanges();
            }

            return _publisher;
        }
        public void DeletePublisher(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (_publisher != null)
            {

                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} not found.");
            }
        }


    }
}
