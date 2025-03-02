using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netigent_Test.Server.Data;
using Netigent_Test.Server.Models;

namespace Netigent_Test.Server.Controllers
{
    [Route("api/statuslevel")]
    [ApiController]
    public class StatusLevelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StatusLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusLevel>>> GetStatusLevels()
        {
            return await _context.StatusLevels.ToListAsync();
        }

        private bool StatusLevelExists(int id)
        {
            return _context.StatusLevels.Any(e => e.Id == id);
        }
    }
}
