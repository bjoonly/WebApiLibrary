using _18._08.ActionResults;
using _18._08.Data.Services;
using _18._08.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublisherService publisherService, ILogger<PublishersController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisherVM)
        {
            var newPublisher = _publisherService.AddPublisher(publisherVM);

            return Created(nameof(AddPublisher), newPublisher);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResult GetPublisher(int id)
        {
            var publisher = _publisherService.GetPublisherById(id);
            if (publisher != null)
            {
                var _responceObject = new CustomActionResultVM()
                {
                    Publisher = publisher
                };
                return new CustomActionResult(_responceObject);
            }
            else
            {
                var _responceObject = new CustomActionResultVM()
                {
                    Exception = new Exception("Publisher not found.")
                };
                return new CustomActionResult(_responceObject);
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]

        public IActionResult GetPublisherData(int id)
        {
            var publisher = _publisherService.GetPublisherData(id);
            if (publisher != null)
            {
                return Ok(publisher);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("get-publishers")]
        public IActionResult GetPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {

                _logger.LogInformation($"SortBy: {sortBy}\tSearchString: {searchString}\tPageNumber: {pageNumber}");
                var allPublishers = _publisherService.GetPublishers(sortBy, searchString, pageNumber);
                return Ok(allPublishers);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
        [HttpPut("edit-publisher/{id}")]
        public IActionResult EditPublisher(int id, [FromBody] PublisherVM publisherVM)
        {
            return Ok(_publisherService.EditPublisher(id, publisherVM));

        }

        [HttpDelete("delete-publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                _publisherService.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
