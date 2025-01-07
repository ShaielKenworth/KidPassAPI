using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class RegistroSalidaApiController : ControllerBase
{
    private readonly EscuelaContext _context;

    public RegistroSalidaApiController(EscuelaContext context)
    {
        _context = context;
    }

    // GET: api/RegistrosSalida
    [HttpGet]
    public async Task<IActionResult> GetRegistrosSalida()
    {
        var registrosSalida = await _context.RegistrosSalida
            .Include(r => r.Alumno)
            .Include(r => r.Profesor)
            .Include(r => r.Tutor)
            .ToListAsync();

        return Ok(registrosSalida); // Devuelve los datos como JSON
    }

    // GET: api/RegistrosSalida/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRegistroSalida(int id)
    {
        var registroSalida = await _context.RegistrosSalida
            .Include(r => r.Alumno)
            .Include(r => r.Profesor)
            .Include(r => r.Tutor)
            .FirstOrDefaultAsync(r => r.RegistroSalidaID == id);

        if (registroSalida == null)
        {
            return NotFound();
        }

        return Ok(registroSalida);
    }

    // POST: api/RegistrosSalida
    [HttpPost]
    public async Task<IActionResult> PostRegistroSalida([FromBody] RegistroSalida registroSalida)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.RegistrosSalida.Add(registroSalida);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRegistroSalida", new { id = registroSalida.RegistroSalidaID }, registroSalida);
    }

    // PUT: api/RegistrosSalida/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistroSalida(int id, [FromBody] RegistroSalida registroSalida)
    {
        if (id != registroSalida.RegistroSalidaID)
        {
            return BadRequest();
        }

        _context.Entry(registroSalida).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegistroSalidaExists(id))
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

    // DELETE: api/RegistrosSalida/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistroSalida(int id)
    {
        var registroSalida = await _context.RegistrosSalida.FindAsync(id);
        if (registroSalida == null)
        {
            return NotFound();
        }

        _context.RegistrosSalida.Remove(registroSalida);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RegistroSalidaExists(int id)
    {
        return _context.RegistrosSalida.Any(e => e.RegistroSalidaID == id);
    }
}
