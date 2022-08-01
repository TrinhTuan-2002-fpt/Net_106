using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET106.Server.Context;
using NET106.Server.Service;
using NET106.Shared.Models;
using NET106.Shared.ViewModels;

namespace NET106.Server.Controllers
{
    [ApiController]
    [Route("api/class")]
    public class ClassController : ControllerBase
    {
        private readonly ISerrvice<ClassViewModel> _serrvice;
        private readonly DatabaseContext _context;

        public ClassController(ISerrvice<ClassViewModel> serrvice,DatabaseContext context)
        {
            _serrvice = serrvice;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetClasss()
        {
            var model = await _context.Classs.Include(c => c.School).ToListAsync();
            var result = model.Select(c => new ClassViewModel
            {
                id = c.Id,
                Schoolid = c.SchoolId,
                Name = c.Name,
                School = c.School.Name
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var model = await _context.Classs.FindAsync(id);
            if (model == null) return NotFound();
            return Ok(new ClassViewModel
            {
                id = model.Id,
                Schoolid = model.SchoolId,
                Name = model.Name,
                School = model.School.Name
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            
        }

        // POST: api/Class
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(ClassViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var major = await _serrvice.Create(new Class()
            {
                Id = model.id,
                Name = model.Name,
                School = majorViewModel.Status,
                SchoolId = majorViewModel.SchoolId
            });

            return CreatedAtAction("GetClasss", new { id = major. }, major);
        }

        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (_context.Classs == null)
            {
                return NotFound();
            }
            var @class = await _context.Classs.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Classs.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return (_context.Classs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
