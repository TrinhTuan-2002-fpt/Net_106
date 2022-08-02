using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ClassController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ISerrvice<ClassViewModel> _service;

        public ClassController(DatabaseContext context, ISerrvice<ClassViewModel> service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Class
        [HttpGet]
        public async Task<IActionResult> GetClasss()
        {
            var cls = await _context.Class.Include(c => c.SchoolId).ToListAsync();
            var model = cls.Select(c => new ClassViewModel
            {
                Id = c.Id,
                SchoolId = c.SchoolId,
                Name = c.Name,
                School = c.School.Name
            });
            return Ok(model);
        }

        // GET: api/Class/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassViewModel>> GetClass(int id)
        {
            var cls = await _context.Class.FindAsync(id);
            if (cls == null) return NotFound();
            return Ok(new ClassViewModel
            {
                Id = cls.Id,
                SchoolId = cls.SchoolId,
                Name = cls.Name,
                School = cls.School.Name
            });
        }

        // PUT: api/Class/
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, ClassViewModel model)
        {
            if (_context.Class == null) return Problem("class null");

            if (!ModelState.IsValid) return BadRequest(model);

            var cls = await _context.Class.FindAsync(id);
            if (cls == null) return NotFound($"{id} not found");
            cls.Name = model.Name;
            cls.SchoolId = model.SchoolId;
            _ = _context.Class.Update(cls);
            await _context.SaveChangesAsync();
            return Ok(new ClassViewModel
            {
                Id = cls.Id,
                SchoolId = cls.SchoolId,
                Name = cls.Name,
                School = cls.School.Name
            });
        }

        // POST: api/Class
        [HttpPost]
        public async Task<IActionResult> PostClass(ClassViewModel model)
        {
          if (_context.Class == null) return Problem("Entity set 'DatabaseContext.Classs'  is null.");
          if (!ModelState.IsValid) return BadRequest((ModelState));
          var cls = new Class
          {
              Id = model.Id,
              Name = model.Name,
              SchoolId = model.SchoolId
          };
          await _context.Class.AddAsync(cls);
          await _context.SaveChangesAsync();
          return CreatedAtAction("GetClass", new { id = cls.Id }, cls);
        }

        // DELETE: api/Class/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (_context.Class == null) return Problem("class null");
            if (!ModelState.IsValid) return BadRequest((ModelState));
            var cls = await _context.Class.FindAsync(id);
            if (cls == null) return NotFound($"{id} not found");

            _context.Class.Remove(cls);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
