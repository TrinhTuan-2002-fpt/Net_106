using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET106.Server.Context;
using NET106.Shared.Models;

namespace NET106.Server.Controllers
{
    
    [ApiController]
    [Route("api/School")]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SchoolController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            if (_context.Schools == null)
            {
                return NotFound();
            }
            return Ok(await _context.Schools.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            if (_context.Schools == null)
            {
                return NotFound();
            }
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.Id)
            {
                return BadRequest();
            }
            _context.Entry(school).State = EntityState.Modified;
            try
            {
                //_context.Schools.Update(school);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/School
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            if (_context.Schools == null)
            {
                return Problem("Entity set 'DatabaseContext.Schools'  is null.");
            }
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/School/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            if (_context.Schools == null)
            {
                return NotFound();
            }
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(int id)
        {
            return (_context.Schools?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
