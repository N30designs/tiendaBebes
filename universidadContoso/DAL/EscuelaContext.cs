using universidadContoso.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace universidadContoso.DAL
{
    public class EscuelaContext: DbContext
    {

        public EscuelaContext() : base("EscuelaContext")
        {

        }


        public DbSet<Estudiante> estudiantes { get; set; }
        public DbSet<Inscripcion> inscripciones { get; set; }
        public DbSet<Curso> cursos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

    }
}