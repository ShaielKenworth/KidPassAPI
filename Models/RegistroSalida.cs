public class RegistroSalida
{
    public int RegistroSalidaID { get; set; }
    public int AlumnoID { get; set; }
    public int ProfesorID { get; set; }
    public int TutorID { get; set; }
    public bool ProfesorVerificado { get; set; }  // Verificación de profesor

    public Alumno Alumno { get; set; }
    public Profesor Profesor { get; set; }
    public Tutor Tutor { get; set; }
}
