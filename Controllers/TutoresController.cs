using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaAPI.Data;

namespace EscuelaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public TutorController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: api/Tutor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutores()
        {
            var tutores = await _context.Tutores.ToListAsync();
            return Ok(tutores);
        }

        // GET: api/Tutor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(int id)
        {
            var tutor = await _context.Tutores
                .FirstOrDefaultAsync(t => t.TutorID == id);

            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }

        // POST: api/Tutor
        [HttpPost]
        public async Task<ActionResult<Tutor>> CreateTutor([FromBody] Tutor tutor)
        {
            if (tutor == null)
            {
                return BadRequest();
            }

            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTutor), new { id = tutor.TutorID }, tutor);
        }

        // PUT: api/Tutor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] Tutor tutor)
        {
            if (id != tutor.TutorID)
            {
                return BadRequest();
            }

            _context.Entry(tutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorExists(id))
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

        // DELETE: api/Tutor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }

            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TutorExists(int id)
        {
            return _context.Tutores.Any(e => e.TutorID == id);
        }
    }
}
