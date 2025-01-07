using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaAPI.Data;  // Asegúrate de que EscuelaContext esté en el espacio de nombres correcto

namespace EscuelaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // Esto convierte el controlador en un controlador API RESTful
    public class ProfesorController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public ProfesorController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: api/profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
        {
            var profesores = await _context.Profesores.ToListAsync();
            return Ok(profesores);  // Devuelve los profesores en formato JSON
        }

        // GET: api/profesor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(p => p.ProfesorID == id);

            if (profesor == null)
            {
                return NotFound();  // Si no se encuentra el profesor, devuelve un error 404
            }

            return Ok(profesor);  // Si el profesor existe, lo devuelve en formato JSON
        }

        // POST: api/profesor
        [HttpPost]
        public async Task<ActionResult<Profesor>> CreateProfesor([Bind("ProfesorID,Nombre,Materia")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Profesores.Add(profesor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProfesor), new { id = profesor.ProfesorID }, profesor);  // Devuelve un código 201 con la URL del recurso creado
            }

            return BadRequest(ModelState);  // Si el modelo no es válido, devuelve un error 400
        }

        // PUT: api/profesor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesor(int id, [Bind("ProfesorID,Nombre,Materia")] Profesor profesor)
        {
            if (id != profesor.ProfesorID)
            {
                return BadRequest();  // Si el ID no coincide, devuelve un error 400
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
                {
                    return NotFound();  // Si el profesor no existe, devuelve un error 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent();  // Devuelve un código 204 si la actualización fue exitosa
        }

        // DELETE: api/profesor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();  // Si el profesor no existe, devuelve un error 404
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve un código 204 si la eliminación fue exitosa
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesores.Any(e => e.ProfesorID == id);
        }
    }
}
