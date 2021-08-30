using _18._08.Data.Services;
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
    public class LogsController : ControllerBase
    {
        private readonly LogService _logService;

        public LogsController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet("get-logs")]
        public IActionResult GetLogs()
        {
            try
            {
                var logs = _logService.GetAllLogsFromDB();
                return Ok(logs);
            }
            catch (Exception)
            {

                return BadRequest("Couldn't load logs.");
            }
        }
    }
}
