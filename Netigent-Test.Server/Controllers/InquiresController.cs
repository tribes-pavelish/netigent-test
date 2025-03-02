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
    [Route("api/inquire")]
    [ApiController]
    public class InquiresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InquiresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inquiries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inquire>>> GetInquiries()
        {
            return await _context.Inquiries.ToListAsync();
        }

        // GET: api/Inquiries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inquire>> GetInquiry(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);

            if (inquiry == null)
            {
                return NotFound();
            }

            return inquiry;
        }

        // PUT: api/Inquiries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInquiry(int id, Inquire inquiry)
        {
            if (id != inquiry.Id)
            {
                return BadRequest();
            }

            _context.Entry(inquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InquiryExists(id))
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

        // POST: api/Inquiries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inquire>> PostInquiry(Inquire inquiry)
        {
            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInquiry", new { id = inquiry.Id }, inquiry);
        }

        // DELETE: api/Inquiries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }

            _context.Inquiries.Remove(inquiry);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Delete successful." });
        }

        private bool InquiryExists(int id)
        {
            return _context.Inquiries.Any(e => e.Id == id);
        }
    }
}
