using universidadContoso.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace universidadContoso.DAL
{
    public class EscuelaContext : DbContext
    {

        public DbSet<Curso> Cursos{ get; set; }
        public DbSet<Departamento> Departamentos{ get; set; }
        public DbSet<Inscripcion> Inscripciones{ get; set; }
        public DbSet<Profesor> Profesores{ get; set; }
        public DbSet<Estudiante> Estudiantes{ get; set; }
        public DbSet<DespachoAsignado> DespachosAsignados{ get; set; }

        public DbSet<Persona> Personas{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Profesores).WithMany(i => i.Cursos)
                .Map(t => t.MapLeftKey("CursoID")
                    .MapRightKey("ProfesorID")
                    .ToTable("CursoProfesor"));
            modelBuilder.Entity<Departamento>().MapToStoredProcedures();
        }
    }
}