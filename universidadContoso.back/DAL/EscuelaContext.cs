using universidadContoso.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace universidadContoso.DAL
{
    public class EscuelaContext : DbContext
    {

        public EscuelaContext() : base("EscuelaContext")
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}