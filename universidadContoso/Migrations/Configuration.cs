namespace universidadContoso.Migrations
{
    using universidadContoso.Models;
    using universidadContoso.DAL;
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

        protected override void Seed(EscuelaContext context)
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


            var profesores = new List<Profesor>
            {
                new Profesor{ Nombre = "Kim",     Apellidos = "Abercrombie",
                    FechaContratacion = DateTime.Parse("11-03-1995") },
                new Profesor { Nombre = "Fadi",    Apellidos = "Fakhouri",
                    FechaContratacion= DateTime.Parse("06-07-2002") },
                new Profesor { Nombre = "Roger",   Apellidos = "Harui",
                    FechaContratacion= DateTime.Parse("01-07-1998") },
                new Profesor { Nombre = "Candace", Apellidos = "Kapoor",
                    FechaContratacion= DateTime.Parse("15-01-2001") },
                new Profesor { Nombre = "Roger",   Apellidos = "Zheng",
                    FechaContratacion= DateTime.Parse("12-02-2004") }
            };
            profesores.ForEach(s => context.Profesores.AddOrUpdate(p => p.Apellidos, s));
            context.SaveChanges();

            var departamentos = new List<Departamento>
            {
                new Departamento { Nombre = "Lenguaje",     Presupuesto = 350000,
                    FechaInicio = DateTime.Parse("01-09-2007"),
                    ProfesorID  = profesores.Single( i => i.Apellidos == "Abercrombie").ID },
                new Departamento { Nombre = "Matemáticas", Presupuesto = 100000,
                    FechaInicio = DateTime.Parse("01-09-2007"),
                    ProfesorID  = profesores.Single( i => i.Apellidos == "Fakhouri").ID },
                new Departamento { Nombre = "Ingenieria", Presupuesto = 350000,
                    FechaInicio = DateTime.Parse("01-09-2007"),
                    ProfesorID  = profesores.Single( i => i.Apellidos == "Harui").ID },
                new Departamento { Nombre = "Económicas",   Presupuesto = 100000,
                    FechaInicio = DateTime.Parse("01-09-2007"),
                    ProfesorID  = profesores.Single( i => i.Apellidos == "Kapoor").ID }
            };
            departamentos.ForEach(s => context.Departamentos.AddOrUpdate(p => p.Nombre, s));
            context.SaveChanges();

            var cursos = new List<Curso>
            {
                new Curso {CursoID = 1050, Nombre = "Química",      Creditos = 3,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Ingenieria").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 4022, Nombre = "Microeconomia", Creditos = 3,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Económicas").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 4041, Nombre = "Macroeconomia", Creditos = 3,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Económicas").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 1045, Nombre = "Cálculo",       Creditos = 4,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Matemáticas").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 3141, Nombre = "Trigonometria",   Creditos = 4,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Matemáticas").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 2021, Nombre = "Composición",    Creditos = 3,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Lenguaje").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
                new Curso {CursoID = 2042, Nombre = "Literatura",     Creditos = 4,
                  DepartamentoID = departamentos.Single( s => s.Nombre == "Lenguaje").DepartamentoID,
                  Profesores = new List<Profesor>()
                },
            };
            cursos.ForEach(s => context.Cursos.AddOrUpdate(p => p.CursoID, s));
            context.SaveChanges();

            var despachosAsignados = new List<DespachoAsignado>
            {
                new DespachoAsignado {
                    ProfesorID = profesores.Single( i => i.Apellidos == "Fakhouri").ID,
                    Ubicacion = "Smith 17" },
                new DespachoAsignado {
                    ProfesorID = profesores.Single( i => i.Apellidos == "Harui").ID,
                    Ubicacion = "Gowan 27" },
                new DespachoAsignado {
                    ProfesorID = profesores.Single( i => i.Apellidos == "Kapoor").ID,
                    Ubicacion = "Thompson 304" },
            };
            despachosAsignados.ForEach(s => context.DespachosAsignados.AddOrUpdate(p => p.ProfesorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Química", "Kapoor");
            AddOrUpdateInstructor(context, "Química", "Harui");
            AddOrUpdateInstructor(context, "Microeconomia", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomia", "Zheng");

            AddOrUpdateInstructor(context, "Cálculo", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometria", "Harui");
            AddOrUpdateInstructor(context, "Composición", "Abercrombie");
            AddOrUpdateInstructor(context, "Literatura", "Abercrombie");

            context.SaveChanges();


            var inscripciones = new List<Inscripcion>
            {
                new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Química" ).CursoID,
                    Nota = Nota.A
                },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Microeconomia" ).CursoID,
                    Nota = Nota.C
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alexander").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Macroeconomia" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                     EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Cálculo" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                     EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Trigonometria" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Alonso").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Composición" ).CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Anand").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Química" ).CursoID
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Anand").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Microeconomia").CursoID,
                    Nota = Nota.B
                 },
                new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Barzdukas").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Química").CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Li").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Composición").CursoID,
                    Nota = Nota.B
                 },
                 new Inscripcion {
                    EstudianteID = estudiantes.Single(s => s.Apellidos == "Justice").ID,
                    CursoID = cursos.Single(c => c.Nombre == "Literatura").CursoID,
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
        void AddOrUpdateInstructor(EscuelaContext context, string nombreCurso, string apellidosProfesor)
        {
            var crs = context.Cursos.SingleOrDefault(c => c.Nombre== nombreCurso);
            var inst = crs.Profesores.SingleOrDefault(i => i.Apellidos== apellidosProfesor);
            if (inst == null)
                crs.Profesores.Add(context.Profesores.Single(i => i.Apellidos== apellidosProfesor));
        }
    }
}