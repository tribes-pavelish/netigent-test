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
    [Route("api/application")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        // GET: api/applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }


        // GET: api/applications/5/inquires
        [HttpGet("{id}/inquires")]
        public async Task<ActionResult<List<Inquire>>> GetInquiresByApplicationId(int id)
        {
            var inquires = _context.Inquiries.Where(item => item.ApplicationId == id).ToList();

            if (inquires == null)
            {
                return NotFound();
            }

            return inquires;
        }


        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Update successful." });
        }

        // POST: api/Applications
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        // DELETE: api/applications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successful." });
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }

        private List<Application> GenerateDummyApplications()
        {
            var applications = new List<Application>();
            for (int i = 1; i <= 20; i++)
            {
                applications.Add(new Application
                {
                    Id = i,
                    AppStatus = i % 2 == 0 ? "Open" : "In Progress",
                    ProjectRef = $"PRJ{i:D3}",
                    ProjectName = $"Project {i}",
                    ProjectLocation = $"Location {i}",
                    OpenDt = DateTime.Now.AddDays(-i * 2),
                    StartDt = DateTime.Now.AddDays(-i),
                    CompleteDt = DateTime.Now.AddDays(i * 5),
                    ProjectValue = 1000000 + (i * 10000),
                    StatusId = i % 3 + 1,
                    Notes = $"Notes for project {i}",
                    Modified = DateTime.Now,
                    IsDeleted = false
                });
            }
            return applications;
        }
    }
}
