using Microsoft.EntityFrameworkCore;

namespace EscuelaAPI.Data
{
    public class EscuelaContext : DbContext
    {
        public EscuelaContext(DbContextOptions<EscuelaContext> options)
            : base(options) // Pasa las opciones al constructor base
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<RegistroSalida> RegistrosSalida { get; set; }

        // Elimina o comenta el método OnConfiguring si lo configuras en Startup.cs o Program.cs
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("YourConnectionStringHere");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroSalida>()
                .HasOne(r => r.Alumno)
                .WithMany(a => a.RegistrosSalida)
                .HasForeignKey(r => r.AlumnoID);

            modelBuilder.Entity<RegistroSalida>()
                .HasOne(r => r.Profesor)
                .WithMany(p => p.RegistrosSalida)
                .HasForeignKey(r => r.ProfesorID);

            modelBuilder.Entity<RegistroSalida>()
                .HasOne(r => r.Tutor)
                .WithMany(t => t.RegistrosSalida)
                .HasForeignKey(r => r.TutorID);
        }
    }
}
