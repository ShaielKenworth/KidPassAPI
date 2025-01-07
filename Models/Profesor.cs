public class Profesor
{
    public int ProfesorID { get; set; }  // Asegúrate de que esta propiedad sea igual al campo en la base de datos
    public string Nombre { get; set; }
    public string Materia { get; set; }

    public ICollection<RegistroSalida> RegistrosSalida { get; set; }  // Relación con RegistrosSalida
}
