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
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM authorVM)
        {
            var newAuthor = _authorService.AddAuthor(authorVM);

            return Created(nameof(AddAuthor), newAuthor);
        }

        [HttpGet("get-author/{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-authors")]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAuthors();

            return Ok(authors);


        }
        [HttpPut("edit-author/{id}")]
        public IActionResult EditAuthor(int id, [FromBody] AuthorVM authorVM)
        {
            return Ok(_authorService.EditAuthor(id, authorVM));

        }

        [HttpDelete("delete-author/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorService.DeleteAuthor(id);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

