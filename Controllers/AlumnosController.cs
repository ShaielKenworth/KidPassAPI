using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscuelaAPI.Data;  // Asegúrate de que EscuelaContext esté en el espacio de nombres correcto

namespace EscuelaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // Esto convierte el controlador en un controlador API RESTful
    public class AlumnoController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: api/alumno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            var alumnos = await _context.Alumnos.ToListAsync();
            return Ok(alumnos);  // Devuelve los alumnos en formato JSON
        }

        // GET: api/alumno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.AlumnoID == id);

            if (alumno == null)
            {
                return NotFound();  // Si no se encuentra el alumno, devuelve un error 404
            }

            return Ok(alumno);  // Si el alumno existe, lo devuelve en formato JSON
        }

        // POST: api/alumno
        [HttpPost]
        public async Task<ActionResult<Alumno>> CreateAlumno([Bind("AlumnoID,Nombre,Apellido,FechaNacimiento")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Alumnos.Add(alumno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAlumno), new { id = alumno.AlumnoID }, alumno);  // Devuelve un código 201 con la URL del recurso creado
            }

            return BadRequest(ModelState);  // Si el modelo no es válido, devuelve un error 400
        }

        // PUT: api/alumno/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlumno(int id, [Bind("AlumnoID,Nombre,Apellido,FechaNacimiento")] Alumno alumno)
        {
            if (id != alumno.AlumnoID)
            {
                return BadRequest();  // Si el ID no coincide, devuelve un error 400
            }

            _context.Entry(alumno).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve un código 204 si la actualización fue exitosa
        }

        // DELETE: api/alumno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();  // Si el alumno no existe, devuelve un error 404
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();  // Devuelve un código 204 si la eliminación fue exitosa
        }
    }
}
