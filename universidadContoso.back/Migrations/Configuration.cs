namespace universidadContoso.Migrations
{
    using universidadContoso.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<universidadContoso.DAL.EscuelaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(universidadContoso.DAL.EscuelaContext context)
        {
            var estudiantes = new List<Estudiante>
            {
                new Estudiante { Nombre = "Carson",   Apellidos = "Alexander",
                    FechaInscripcion = DateTime.Parse("2010-09-01") },
                new Estudiante { Nombre = "Meredith", Apellidos = "Alonso",
                    FechaInscripcion = DateTime.Parse("2012-09-01") },
                new Estudiante { Nombre = "Arturo",   Apellidos = "Anand",
                    FechaInscripcion = DateTime.Parse("2013-09-01") },
                new Estudiante { Nombre = "Gytis",    Apellidos = "Barzdukas",
                    FechaInscripcion = DateTime.Parse("2012-09-01") },
                new Estudiante { Nombre = "Yan",      Apellidos = "Li",
                    FechaInscripcion = DateTime.Parse("2012-09-01") },
                new Estudiante { Nombre = "Peggy",    Apellidos = "Justice",
                    FechaInscripcion = DateTime.Parse("2011-09-01") },
                new Estudiante { Nombre = "Laura",    Apellidos = "Norman",
                    FechaInscripcion = DateTime.Parse("2013-09-01") },
                new Estudiante { Nombre = "Nino",     Apellidos = "Olivetto",
                    FechaInscripcion = DateTime.Parse("2005-08-11") }
            };
            estudiantes.ForEach(s => context.Estudiantes.AddOrUpdate(p => p.Apellidos, s));
            context.SaveChanges();

            var cursos = new List<Curso>
            {
                new Curso {CursoID = 1050, Nombre = "Chemistry",      Creditos = 3, },
                new Curso {CursoID = 4022, Nombre = "Microeconomics", Creditos = 3, },
                new Curso {CursoID = 4041, Nombre = "Macroeconomics", Creditos = 3, },
                new Curso {CursoID = 1045, Nombre = "Calculus",       Creditos = 4, },
                new Curso {CursoID = 3141, Nombre = "Trigonometry",   Creditos = 4, },
                new Curso {CursoID = 2021, Nombre = "Composition",    Creditos = 3, },
                new Curso {CursoID = 2042, Nombre = "Literature",     Creditos = 4, }
            };
            cursos.ForEach(s => context.Cursos.AddOrUpdate(p => p.Nombre, s));
            context.SaveChanges();

            var inscripciones = new List<Inscripcion>
            {
                new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Chemistry" ).CursoID,
                    Nota = Nota.A
                },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Microeconomics" ).CursoID,
                    Nota = Nota.C
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Macroeconomics" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                     EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Calculus" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                     EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Trigonometry" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Composition" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Anand").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Chemistry" ).CursoID
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Anand").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Microeconomics").CursoID,
                    Nota = Nota.B
                 },
                new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Barzdukas").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Chemistry").CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Li").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Composition").CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Justice").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Literature").CursoID,
                    Nota = Nota.B
                 }
            };

            foreach (Inscripcion e in inscripciones)
            {
                var enrollmentInDataBase = context.Inscripciones.Where(
                    s =>
                         s.Estudiante.ID == e.EstudianteID &&
                         s.Curso.CursoID == e.CursoID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Inscripciones.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}