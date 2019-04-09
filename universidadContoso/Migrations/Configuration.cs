namespace universidadContoso.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using universidadContoso.Models;
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
                new Estudiante
                {
                    nombre = "Carson",
                    apellido = "Alexander",
                    fechaInscripcion = DateTime.Parse("2010-09-01")
                },
                new Estudiante
                {
                    nombre = "Meredith",
                    apellido = "Alonso",
                    fechaInscripcion = DateTime.Parse("2012-09-01")
                },
                new Estudiante
                {
                    nombre = "Arturo",
                    apellido = "Anand",
                    fechaInscripcion = DateTime.Parse("2013-09-01")
                },
                new Estudiante
                {
                    nombre = "Gytis",
                    apellido = "Barzdukas",
                    fechaInscripcion = DateTime.Parse("2012-09-01")
                },
                new Estudiante
                {
                    nombre = "Yan",
                    apellido = "Li",
                    fechaInscripcion = DateTime.Parse("2012-09-01")
                },
                new Estudiante
                {
                    nombre = "Peggy",
                    apellido = "Justice",
                    fechaInscripcion = DateTime.Parse("2011-09-01")
                },
                new Estudiante
                {
                    nombre = "Laura",
                    apellido = "Norman",
                    fechaInscripcion = DateTime.Parse("2013-09-01")
                },
                new Estudiante
                {
                    nombre = "Nino",
                    apellido = "Olivetto",
                    fechaInscripcion = DateTime.Parse("2005-08-11")
                }
            };

            estudiantes.ForEach(s => context.estudiantes.AddOrUpdate(p => p.apellido, s));
            context.SaveChanges();

            var cursos = new List<Curso>
            {
                new Curso { cursoID = 1050, titulo = "Química", creditos = 3, },
                new Curso { cursoID = 4022, titulo = "Microeconomía", creditos = 3, },
                new Curso { cursoID = 4041, titulo = "Macroeconomía", creditos = 3, },
                new Curso { cursoID = 1045, titulo = "Cálculo", creditos = 4, },
                new Curso { cursoID = 3141, titulo = "Trigonometria", creditos = 4, },
                new Curso { cursoID = 2021, titulo = "Composición", creditos = 3, },
                new Curso { cursoID = 2042, titulo = "Literatura", creditos = 4, }
            };

            cursos.ForEach(s => context.cursos.AddOrUpdate(p => p.titulo, s));
            context.SaveChanges();

            var inscripciones = new List<Inscripcion>
            {
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Alexander").id,
                cursoID = cursos.Single(c => c.titulo == "Química" ).cursoID,
                grado = Grado.A
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Alexander").id,
                cursoID = cursos.Single(c => c.titulo == "Microeconomía" ).cursoID,
                grado = Grado.C
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Alexander").id,
                cursoID = cursos.Single(c => c.titulo == "Macroeconomía" ).cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                    estudianteID = estudiantes.Single(s => s.apellido == "Alonso").id,
                cursoID = cursos.Single(c => c.titulo == "Cálculo" ).cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                    estudianteID = estudiantes.Single(s => s.apellido == "Alonso").id,
                cursoID = cursos.Single(c => c.titulo == "Trigonometria" ).cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Alonso").id,
                cursoID = cursos.Single(c => c.titulo == "Composición" ).cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Anand").id,
                cursoID = cursos.Single(c => c.titulo == "Química" ).cursoID
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Anand").id,
                cursoID = cursos.Single(c => c.titulo == "Microeconomía").cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Barzdukas").id,
                cursoID = cursos.Single(c => c.titulo == "Química").cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Li").id,
                cursoID = cursos.Single(c => c.titulo == "Composición").cursoID,
                grado = Grado.B
                },
                new Inscripcion {
                estudianteID = estudiantes.Single(s => s.apellido == "Justice").id,
                cursoID = cursos.Single(c => c.titulo == "Literatura ").cursoID,
                grado = Grado.B }
             };

            foreach (Inscripcion e in inscripciones)
            {
                var inscripcionesDataBase = context.inscripciones.Where(
                    s =>
                        s.estudiante.id == e.estudianteID &&
                        s.curso.cursoID == e.cursoID).SingleOrDefault();
                if (inscripcionesDataBase == null)
                {
                    context.inscripciones.Add(e);
                }
            }

            context.SaveChanges();
        }

    }
}
