using _18._08.Data.Models;
using _18._08.Data.Services;
using _18._08.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);

            return Ok();

        }
        [HttpGet("get-books")]
        public IActionResult GetBooks()
        {
            return Ok(_booksService.GetBooks());

        }

        //[HttpGet("get-book/{id}")]
        //public IActionResult GetBook(int id)
        //{
        //    var _book = _booksService.GetBookById(id);
        //    if (_book != null)
        //    {
        //        return Ok(_book);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet("get-book-with-authors/{id}")]
        public IActionResult GetBookWithAuthors(int id)
        {
            var book = _booksService.GetBookWithAuthorsById(id);
            if (book != null)
            {
                return Ok(book);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("edit-book/{id}")]
        public IActionResult EditBook(int id, [FromBody] BookVM bookVM)
        {
            var _book = _booksService.EditBook(id, bookVM);
            return Ok(_book);
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _booksService.DeleteBook(id);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
