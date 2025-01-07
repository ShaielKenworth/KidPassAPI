public class Alumno
{
    public int AlumnoID { get; set; }  // Asegúrate de que esta propiedad sea igual al campo en la base de datos
    public string Nombre { get; set; }
    public string Grado { get; set; }

    public ICollection<RegistroSalida> RegistrosSalida { get; set; }  // Relación con RegistrosSalida
}
