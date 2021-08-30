using _18._08.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18._08.Data.Services
{
    public class LogService
    {
        private readonly AppDbContext _context;
        public LogService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDB()
        {
            return _context.Logs.ToList();
        }
    }
}
